using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Core.Netcode.Packets
{
	public class SetImmunePacket : PlayerPacket
	{
		readonly short time;
		readonly short longTime;
		readonly bool noBlink;
		readonly int cooldownID;
		readonly string combatText;
		readonly Color combatTextColor;
		readonly bool dramatic;
		readonly bool dot;

		//For reflection
		public SetImmunePacket() { }

		public SetImmunePacket(Player player, short time, short longTime, bool noBlink, int cooldownID, string combatText, Color combatTextColor, bool dramatic, bool dot) : base(player)
		{
			this.time = time;
			this.longTime = longTime;
			this.noBlink = noBlink;
			this.cooldownID = cooldownID;
			this.combatText = combatText;
			this.combatTextColor = combatTextColor;
			this.dramatic = dramatic;
			this.dot = dot;
		}

		protected override void PostSend(BinaryWriter writer, Player player)
		{
			writer.Write(time);
			writer.Write(longTime);
			writer.Write((sbyte)cooldownID);

			bool hasText = !string.IsNullOrEmpty(combatText);
			BitsByte bits = new BitsByte(noBlink, hasText, dramatic, dot);

			writer.Write((byte)bits);

			if (hasText)
			{
				writer.Write(combatText);
				writer.WriteRGB(combatTextColor);
			}
		}

		protected override void PostReceive(BinaryReader reader, int sender, Player player)
		{
			short time = reader.ReadInt16();
			short longTime = reader.ReadInt16();
			sbyte cooldownID = reader.ReadSByte();

			BitsByte bits = reader.ReadByte();
			bool noBlink = bits[0];
			bool hasText = bits[1];
			bool dramatic = bits[2];
			bool dot = bits[3];

			string combatText = "";
			Color combatTextColor = default(Color);
			if (hasText)
			{
				combatText = reader.ReadString();
				combatTextColor = reader.ReadRGB();
			}

			//Only server should re-broadcast it
			bool broadcast = Main.netMode == NetmodeID.Server;
			player.SetImmune(time, longTime, noBlink, cooldownID, combatText, combatTextColor, dramatic, dot, broadcast);
		}
	}
}
