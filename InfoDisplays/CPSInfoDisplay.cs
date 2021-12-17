using System;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.InfoDisplays
{
	public class CPSInfoDisplay : InfoDisplay
	{
		public override bool Active() {
			return true; //TODO Usually some accessory flag here
		}

		public override string DisplayValue() {
			Player player = Main.LocalPlayer;
			return "" + Math.Floor(player.GetModPlayer<ClickerPlayer>().clickerPerSecond) + " " + DisplayName;
		}
	}
}
