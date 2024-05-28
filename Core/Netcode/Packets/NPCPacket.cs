using System.IO;
using Terraria;

namespace ClickerClass.Core.Netcode.Packets
{
	/// <summary>
	/// Tied to an NPC
	/// </summary>
	public abstract class NPCPacket : MPPacket
	{
		protected readonly byte NPCWhoAmI;
		protected readonly int NPCType;

		//For reflection
		public NPCPacket() { }

		public NPCPacket(NPC npc)
		{
			NPCWhoAmI = (byte)npc.whoAmI;
			NPCType = npc.type;
		}

		public sealed override void Send(BinaryWriter writer)
		{
			writer.Write((byte)NPCWhoAmI);
			writer.Write7BitEncodedInt(NPCType);
			PostSend(writer, Main.npc[NPCWhoAmI]);
		}

		protected abstract void PostSend(BinaryWriter writer, NPC npc);

		public sealed override void Receive(BinaryReader reader, int sender)
		{
			byte whoAmI = reader.ReadByte();
			int type = reader.Read7BitEncodedInt();

			NPC npc;
			if (whoAmI >= Main.maxNPCs || Main.npc[whoAmI].type != type)
			{
				npc = new NPC();
			}
			else
			{
				npc = Main.npc[whoAmI];
			}

			PostReceive(reader, sender, npc);
		}

		protected abstract void PostReceive(BinaryReader reader, int sender, NPC npc);
	}
}
