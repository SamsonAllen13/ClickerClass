using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.InfoDisplays
{
	public class CPSInfoDisplay : InfoDisplay
	{
		public static LocalizedText DisplayValueText { get; private set; }

		public override void SetStaticDefaults()
		{
			DisplayValueText = this.GetLocalization("DisplayValue");
		}

		public override bool Active()
		{
			Player player = Main.LocalPlayer;
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			return clickerPlayer.clickerSelected && clickerPlayer.accButtonMasher;
		}

		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			Player player = Main.LocalPlayer;
			var val = player.GetModPlayer<ClickerPlayer>().clickerPerSecondDisplay;
			if (val == 0)
			{
				displayColor = InactiveInfoTextColor;
			}
			return DisplayValueText.Format(val);
		}
	}
}
