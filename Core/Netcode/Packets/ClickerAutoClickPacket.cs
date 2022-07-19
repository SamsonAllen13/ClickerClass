using System.IO;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Core.Netcode.Packets
{
	public class ClickerAutoClickPacket : PlayerPacket
	{
		private readonly bool toggle = false;

		//For reflection
		public ClickerAutoClickPacket() { }

		public ClickerAutoClickPacket(Player player, bool toggle) : base(player)
		{
			this.toggle = toggle;
		}

		protected override void PostSend(BinaryWriter writer, Player player)
		{
			writer.Write((bool)toggle);
		}

		protected override void PostReceive(BinaryReader reader, int sender, Player player)
		{
			bool toggle = reader.ReadBoolean();

			player.GetModPlayer<ClickerPlayer>().clickerAutoClick = toggle;

			if (Main.netMode == NetmodeID.Server)
			{
				new ClickerAutoClickPacket(player, toggle).Send(from: sender);
			}
		}
	}
}
