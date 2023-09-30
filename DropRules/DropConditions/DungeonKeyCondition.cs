using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;

namespace ClickerClass.DropRules.DropConditions
{
	public class DungeonKeyCondition : IItemDropRuleCondition
	{
		public static LocalizedText DescriptionText { get; private set; }

		public static bool ValidLikeSeasonalDrop(DropAttemptInfo info)
		{
			NPC npc = info.npc;
			return npc.lifeMax > 1 && npc.damage > 0 && !npc.friendly && npc.type != NPCID.Slimer && npc.type != NPCID.MeteorHead && npc.value > 0f;
		}

		public static LocalizedText GetDescriptionText(string name)
		{
			return Language.GetOrRegister(ClickerClass.mod.GetLocalizationKey($"DropConditions.{name}.Description"));
		}

		public DungeonKeyCondition()
		{
			DescriptionText ??= GetDescriptionText(GetType().Name);
		}

		public bool CanDrop(DropAttemptInfo info)
		{
			if (info.IsInSimulation || !Main.hardMode || !ValidLikeSeasonalDrop(info))
			{
				return false;
			}
			return info.player.ZoneDungeon;
		}

		public bool CanShowItemDropInUI()
		{
			return true;
		}

		public string GetConditionDescription()
		{
			return DescriptionText.ToString();
		}
	}
}
