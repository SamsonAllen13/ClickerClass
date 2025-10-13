using Terraria.Achievements;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace ClickerClass.Achievements.Challenger
{
	public class DigitDestroyerAchievement : ModAchievement
	{
		public CustomIntCondition Condition { get; private set; }

		public override void SetStaticDefaults()
		{
			Achievement.SetCategory(AchievementCategory.Challenger);

			Condition = AddIntCondition(1000000);
		}
	}
}
