using Terraria;
using Terraria.Localization;

namespace ClickerClass
{
	public static class ClickerConditions
	{
		public static readonly string CommonKey = "Conditions.";
		public static readonly Condition ClickerSelected = new Condition(Language.GetOrRegister(ClickerClass.mod.GetLocalizationKey($"{CommonKey}ClickerSelected")), () => Main.LocalPlayer.GetModPlayer<ClickerPlayer>().clickerSelected);

		//Alternatively hardcode each amount
		/// <summary>
		/// Returns a new condition specifically with the amount listed.
		/// </summary>
		public static Condition ClickerTotalExceeds(int amount)
		{
			return new Condition(Language.GetOrRegister(ClickerClass.mod.GetLocalizationKey($"{CommonKey}ClickerTotalExceeds")).WithFormatArgs(amount), () => Main.LocalPlayer.GetModPlayer<ClickerPlayer>().clickerTotal >= amount);
		}
	}
}
