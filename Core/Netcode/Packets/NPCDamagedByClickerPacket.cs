using ClickerClass.Core.Handlers.ClickerDamageDropHandler;
using System.IO;
using Terraria;

namespace ClickerClass.Core.Netcode.Packets
{
	public class NPCDamagedByClickerPacket : NPCPacket
	{
		private readonly int playerWhoAmI;

		//For reflection
		public NPCDamagedByClickerPacket() { }

		public NPCDamagedByClickerPacket(NPC npc, int playerWhoAmI) : base(npc)
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
			ClickerNPCDropGlobalNPC.SetDamagedByClicker(npc, playerWhoAmI);
		}
	}
}
