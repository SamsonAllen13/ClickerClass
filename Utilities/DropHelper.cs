using ClickerClass.DropRules;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ClickerClass.Utilities
{
	internal static class DropHelper
	{
		//Mimics ItemDropRule.ExpertGetsRerolls but with custom % instead of #rerolls
		//Wrapper for other rules ontop
		internal static void NPCExpertGetsRerolls(ILoot loot, int itemId, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1, int chanceNumerator = 1, IItemDropRule ruleExpert = null, IItemDropRule ruleNormal = null, float rerollChance = 0.5f)
		{
			//Since the conditions are exclusive, only one of them will show up
			IItemDropRule expertRule = new LeadingConditionRule(new Conditions.IsExpert());
			IItemDropRule ruleToAdd = expertRule;
			if (ruleExpert != null)
			{
				ruleToAdd = ruleExpert; //If a rule is specified, use that to add it (Always add the "baseline" rule first)
				expertRule = ruleToAdd.OnSuccess(expertRule);
			}
			expertRule.OnSuccess(new CommonDropWithReroll(itemId, chanceDenominator, minimumDropped, maximumDropped, chanceNumerator, rerollChance));
			loot.Add(ruleToAdd);

			//Vanilla example
			//Conditions.IsPumpkinMoon condition2 = new Conditions.IsPumpkinMoon();
			//Conditions.FromCertainWaveAndAbove condition3 = new Conditions.FromCertainWaveAndAbove(15);

			//LeadingConditionRule entry = new LeadingConditionRule(condition2);
			//LeadingConditionRule ruleToChain = new LeadingConditionRule(condition3);
			//npcLoot.Add(entry).OnSuccess(ruleToChain).OnSuccess(ItemDropRule.Common(1856));

			IItemDropRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
			ruleToAdd = notExpertRule;
			if (ruleNormal != null)
			{
				ruleToAdd = ruleNormal;
				notExpertRule = ruleToAdd.OnSuccess(notExpertRule);
			}
			notExpertRule.OnSuccess(new CommonDrop(itemId, chanceDenominator, minimumDropped, maximumDropped, chanceNumerator));
			loot.Add(ruleToAdd);
		}
	}
}
