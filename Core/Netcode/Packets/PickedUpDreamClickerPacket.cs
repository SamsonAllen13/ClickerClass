using System.IO;
using Terraria;

namespace ClickerClass.Core.Netcode.Packets
{
	public class PickedUpDreamClickerPacket : PlayerPacket
	{
		//For reflection
		public PickedUpDreamClickerPacket() { }

		public PickedUpDreamClickerPacket(Player player) : base(player)
		{

		}

		protected override void PostSend(BinaryWriter writer, Player player)
		{
			//No-op
		}

		protected override void PostReceive(BinaryReader reader, int sender, Player player)
		{
			player.GetModPlayer<ClickerPlayer>().pickedUpDreamClicker = true;
		}
	}
}
