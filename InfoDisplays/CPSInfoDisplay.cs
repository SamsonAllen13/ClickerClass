using System;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.InfoDisplays
{
	public class CPSInfoDisplay : InfoDisplay
	{
		public override bool Active() {
			Player player = Main.LocalPlayer;
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			return clickerPlayer.clickerSelected && clickerPlayer.accButtonMasher;
		}

		public override string DisplayValue() {
			Player player = Main.LocalPlayer;
			return "" + Math.Floor(player.GetModPlayer<ClickerPlayer>().clickerPerSecond) + " " + DisplayName;
		}
	}
}
