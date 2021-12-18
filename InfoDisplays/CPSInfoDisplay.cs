using System;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.InfoDisplays
{
	public class CPSInfoDisplay : InfoDisplay
	{
		public override bool Active() {
			Player player = Main.LocalPlayer;
			return player.GetModPlayer<ClickerPlayer>().accButtonMasher;
		}

		public override string DisplayValue() {
			Player player = Main.LocalPlayer;
			return "" + Math.Floor(player.GetModPlayer<ClickerPlayer>().clickerPerSecond) + " " + DisplayName;
		}
	}
}
