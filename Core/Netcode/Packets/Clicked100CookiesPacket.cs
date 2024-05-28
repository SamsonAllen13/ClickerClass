using System.IO;
using Terraria;

namespace ClickerClass.Core.Netcode.Packets
{
	public class Clicked100CookiesPacket : PlayerPacket
	{
		//For reflection
		public Clicked100CookiesPacket() { }

		public Clicked100CookiesPacket(Player player) : base(player)
		{

		}

		protected override void PostSend(BinaryWriter writer, Player player)
		{

		}

		protected override void PostReceive(BinaryReader reader, int sender, Player player)
		{
			player.GetModPlayer<ClickerPlayer>().paintingCondition_Clicked100Cookies = true;
		}
	}
}
