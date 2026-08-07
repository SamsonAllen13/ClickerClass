using Terraria.GameContent.ItemDropRules;
using ClickerClass.Utilities;

namespace ClickerClass.DropRules
{
	//Copy of DropLocalPerClientAndResetsNPCMoneyTo0 without the moneyto0, and with the condition applying individually per player
	public class DropLocalPerClientConditionPerPlayer : ItemDropWithConditionRule
	{
		public DropLocalPerClientConditionPerPlayer(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum, IItemDropRuleCondition optionalCondition = null, int chanceNumerator = 1)
			: base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, optionalCondition, chanceNumerator)
		{

		}

		public override bool CanDrop(DropAttemptInfo info)
		{
			//Condition evaluated per-player
			//return base.CanDrop(info);

			return true;
		}

		public bool GetCondition(DropAttemptInfo info)
		{
			return condition.CanDrop(info);
		}

		public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			ItemDropAttemptResult result;
			if (info.rng.Next(chanceDenominator) < chanceNumerator)
			{
				NPCHelper.DropItemInstanced(info, itemId, info.rng.Next(amountDroppedMinimum, amountDroppedMaximum + 1), condition: GetCondition);
				result = default(ItemDropAttemptResult);
				result.State = ItemDropAttemptResultState.Success;
				return result;
			}

			result = default(ItemDropAttemptResult);
			result.State = ItemDropAttemptResultState.FailedRandomRoll;
			return result;
		}
	}
}
