using Terraria;
using Terraria.UI;
using ClickerClass.Utilities;

namespace ClickerClass.UI
{
	internal class SMedalGauge : MedalGaugeBase
	{
		public SMedalGauge() : base("ClickerClass: S Medal Gauge", InterfaceScaleType.UI) { }

		protected override int GetValue()
		{
			Player player = Main.LocalPlayer;
			ClickerPlayer clickerPlayer = player.GetClickerPlayer();
			return clickerPlayer.accSMedalAmount;
		}

		protected override int TextColor()
		{
			return Terraria.ID.ItemRarityID.Green;
		}

		protected override string TexturePath => "UI/SMedalGauge_Sheet";
	}
}
