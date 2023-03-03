using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.InfoDisplays
{
	public class CPSInfoDisplay : InfoDisplay
	{
		public LocalizedText DisplayValueText;

		public override void SetStaticDefaults()
		{
			DisplayValueText = this.GetLocalization("DisplayValue");
		}

		public override bool Active() {
			Player player = Main.LocalPlayer;
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			return clickerPlayer.clickerSelected && clickerPlayer.accButtonMasher;
		}

		public override string DisplayValue(ref Color displayColor)
		{
			Player player = Main.LocalPlayer;
			var val = Math.Floor(player.GetModPlayer<ClickerPlayer>().clickerPerSecond);
			if (val == 0)
			{
				displayColor = InactiveInfoTextColor;
			}
			//TODO currently doesn't work, crashes. both "{$Mods.ClickerClass.Common.Tooltips.Clicks} per second" and "{0} {^0:click;clicks} per second". no pluralization works however
			return DisplayValueText.WithFormatArgs(val).ToString();
		}
	}
}
