using ClickerClass.Achievements.Challenger;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Utilities
{
	public class AchievementHelper
	{
		public static void UpdateDigitDestroyerAchievement(ClickerPlayer player)
		{
			if (Main.netMode == NetmodeID.Server) return;

			var achievement = ModContent.GetInstance<DigitDestroyerAchievement>();
			if (achievement.Condition.IsCompleted) return;
			else achievement.Condition.Value = player.clickerTotal;
		}
	}
}
