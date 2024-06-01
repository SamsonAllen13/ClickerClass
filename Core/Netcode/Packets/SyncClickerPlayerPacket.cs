using System.IO;
using Terraria;

namespace ClickerClass.Core.Netcode.Packets
{
	public class SyncClickerPlayerPacket : PlayerPacket
	{
		private readonly bool clickerAutoClick = false;
		private readonly bool paintingCondition_MoonLordDefeatedWithClicker = false;
		private readonly bool paintingCondition_Clicked100Cookies = false;
		//Add more fields here and to the ctor/write/read

		//For reflection
		public SyncClickerPlayerPacket() { }

		public SyncClickerPlayerPacket(ClickerPlayer clickerPlayer) : base(clickerPlayer.Player)
		{
			clickerAutoClick = clickerPlayer.clickerAutoClick;
			paintingCondition_MoonLordDefeatedWithClicker = clickerPlayer.paintingCondition_MoonLordDefeatedWithClicker;
			paintingCondition_Clicked100Cookies = clickerPlayer.paintingCondition_Clicked100Cookies;
		}

		protected override void PostSend(BinaryWriter writer, Player player)
		{
			BitsByte flags = new BitsByte();
			flags[0] = clickerAutoClick;
			flags[1] = paintingCondition_MoonLordDefeatedWithClicker;
			flags[2] = paintingCondition_Clicked100Cookies;
			writer.Write((byte)flags);
		}

		protected override void PostReceive(BinaryReader reader, int sender, Player player)
		{
			BitsByte flags = reader.ReadByte();
			bool clickerAutoClick = flags[0];
			bool paintingCondition_MoonLordDefeatedWithClicker = flags[1];
			bool paintingCondition_Clicked100Cookies = flags[2];

			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerAutoClick = clickerAutoClick;
			clickerPlayer.paintingCondition_MoonLordDefeatedWithClicker = paintingCondition_MoonLordDefeatedWithClicker;
			clickerPlayer.paintingCondition_Clicked100Cookies = paintingCondition_Clicked100Cookies;
		}
	}
}
