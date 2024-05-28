using System.IO;
using Terraria;

namespace ClickerClass.Core.Netcode.Packets
{
	public class MoonLordDefeatedWithClickerPacket : PlayerPacket
	{
		//For reflection
		public MoonLordDefeatedWithClickerPacket() { }

		public MoonLordDefeatedWithClickerPacket(Player player) : base(player)
		{

		}

		protected override void PostSend(BinaryWriter writer, Player player)
		{

		}

		protected override void PostReceive(BinaryReader reader, int sender, Player player)
		{
			player.GetModPlayer<ClickerPlayer>().paintingCondition_MoonLordDefeatedWithClicker = true;
		}
	}
}
