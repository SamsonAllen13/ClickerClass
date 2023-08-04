using System.IO;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Core.Netcode.Packets
{
	public class TranscendenceDodgePacket : PlayerPacket
	{
		//For reflection
		public TranscendenceDodgePacket() { }

		public TranscendenceDodgePacket(Player player) : base(player)
		{

		}

		protected override void PostSend(BinaryWriter writer, Player player)
		{
			//No-op
		}

		protected override void PostReceive(BinaryReader reader, int sender, Player player)
		{
			//Only broadcast if this is the server (otherwise a receiving client would rebroadcast it again)
			bool broadcast = Main.netMode == NetmodeID.Server;
			player.GetModPlayer<ClickerPlayer>().TranscendenceDodge(broadcast);
		}
	}
}
