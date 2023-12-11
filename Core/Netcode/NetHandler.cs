using ClickerClass.Core.Netcode.Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace ClickerClass.Core.Netcode
{
	/// <summary>
	/// Manages MPPackets, handles sending/receiving
	/// </summary>
	public static class NetHandler
	{
		private static List<MPPacket> Packets { get; set; }
		public static Dictionary<Type, byte> ID { get; private set; }

		public static Mod Mod { get; private set; }

		public static void Load()
		{
			Packets = new List<MPPacket>();
			ID = new Dictionary<Type, byte>();

			Mod = ModContent.GetInstance<ClickerClass>();

			RegisterPackets();
		}

		public static void Unload()
		{
			Packets = null;
			ID = null;
			Mod = null;
		}

		private static void RegisterPackets()
		{
			Type mpPacketType = typeof(MPPacket);
			IEnumerable<Type> mpPacketTypes = AssemblyManager.GetLoadableTypes(Mod.Code).Where(t => !t.IsAbstract && t.IsSubclassOf(mpPacketType));

			foreach (var type in mpPacketTypes)
			{
				MPPacket packet = (MPPacket)Activator.CreateInstance(type);

				int count = Packets.Count;

				Packets.Add(packet);
				if (count > byte.MaxValue)
				{
					throw new Exception($"Packet limit of {byte.MaxValue} reached!");
				}

				byte id = (byte)count;
				ID[type] = id;
			}
		}

		public static void HandlePackets(BinaryReader reader, int sender)
		{
			byte ID = reader.ReadByte();

			try
			{
				if (ID >= Packets.Count)
				{
					return;
				}

				MPPacket packet = Packets[ID];

				packet.Receive(reader, sender);
			}
			catch (Exception e)
			{
				Mod.Logger.Warn($"Exception handling packet #{ID}: {e}");
			}
		}

		/// <summary>
		/// Sends a packet of this given type
		/// </summary>
		/// <param name="to">The client whoAmI, -1 if everyone</param>
		/// <param name="from">The client the packet originated from</param>
		/// <param name="bcCondition">Decide if the packet should be broadcasted to a specific player</param>
		public static void Send<T>(T packet, int to = -1, int from = -1, Func<Player, bool> bcCondition = null) where T : MPPacket
		{
			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				//No packets in singleplayer
				return;
			}

			Type type = packet.GetType(); //not typeof(T) as the shortcut exists in the MPPacket class and that doesn't exist anywhere
			ModPacket modPacket = Mod.GetPacket();

			modPacket.Write((byte)ID[type]); //Write the ID first
			packet.Send(modPacket); //Let the packet write its data

			try
			{
				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					//to/from doesn't matter for client, it always goes to server
					modPacket.Send();
				}
				else if (to != -1) //Server and specific client
				{
					modPacket.Send(to, from);
				}
				else //Server and broadcast
				{
					for (int i = 0; i < Main.maxPlayers; i++)
					{
						if (i != from && Netplay.Clients[i].State >= 10 && (bcCondition?.Invoke(Main.player[i]) ?? true))
						{
							modPacket.Send(i);
						}
					}
				}
			}
			catch { }
		}
	}
}
