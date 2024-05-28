using ClickerClass.Items.Placeable;
using System.IO;
using Terraria;

namespace ClickerClass.Core.Netcode.Packets
{
	public class MoonLordDamagedByClickerPacket : NPCPacket
	{
		private readonly int playerWhoAmI;

		//For reflection
		public MoonLordDamagedByClickerPacket() { }

		public MoonLordDamagedByClickerPacket(NPC npc, int playerWhoAmI) : base(npc)
		{
			this.playerWhoAmI = playerWhoAmI;
		}

		protected override void PostSend(BinaryWriter writer, NPC npc)
		{
			writer.Write((byte)playerWhoAmI);
		}

		protected override void PostReceive(BinaryReader reader, int sender, NPC npc)
		{
			int playerWhoAmI = reader.ReadByte();
			MoonLordPaintingNPC.SetDamagedByClicker(npc, playerWhoAmI);
		}
	}
}
