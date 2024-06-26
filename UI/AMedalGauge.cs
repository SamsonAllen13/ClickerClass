﻿using ClickerClass.Utilities;
using Terraria;
using Terraria.UI;

namespace ClickerClass.UI
{
	internal class AMedalGauge : MedalGaugeBase
	{
		public AMedalGauge() : base("ClickerClass: A Medal Gauge", InterfaceScaleType.UI) { }

		protected override int GetValue()
		{
			Player player = Main.LocalPlayer;
			ClickerPlayer clickerPlayer = player.GetClickerPlayer();
			return clickerPlayer.accAMedalAmount;
		}

		protected override int TextColor()
		{
			Player player = Main.LocalPlayer;
			ClickerPlayer clickerPlayer = player.GetClickerPlayer();
			return clickerPlayer.AccSMedal ? Terraria.ID.ItemRarityID.Orange : Terraria.ID.ItemRarityID.Pink;
		}

		protected override string TexturePath => "UI/AMedalGauge_Sheet";
	}
}
