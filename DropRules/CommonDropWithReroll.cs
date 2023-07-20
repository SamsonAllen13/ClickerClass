using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace ClickerClass.DropRules
{
	public class CommonDropWithReroll : CommonDrop
	{
		readonly float rerollChance;

		//Make all spawn chances more likely on expert mode
		//Formula:
		//expertmode% = normalmode% * (1 + (1 - normalmode%) * rerollChance)
		//Mimics CommonDropWithRerolls but with custom % instead of #rerolls
		public CommonDropWithReroll(int itemId, int chanceDenominator = 1, int amountDroppedMinimum = 1, int amountDroppedMaximum = 1, int chanceNumerator = 1, float rerollChance = 0.5f)
			: base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, chanceNumerator)
		{
			this.rerollChance = rerollChance;
		}

		public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			bool flag = Roll(info);
			if (!flag && info.rng.NextFloat() < rerollChance)
			{
				flag = Roll(info);
			}

			ItemDropAttemptResult result;
			if (flag)
			{
				CommonCode.DropItem(info, itemId, info.rng.Next(amountDroppedMinimum, amountDroppedMaximum + 1));
				result = default(ItemDropAttemptResult);
				result.State = ItemDropAttemptResultState.Success;
				return result;
			}

			result = default(ItemDropAttemptResult);
			result.State = ItemDropAttemptResultState.FailedRandomRoll;
			return result;

			bool Roll(DropAttemptInfo info)
			{
				return info.player.RollLuck(chanceDenominator) < chanceNumerator;
			}
		}

		public override void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float chance = chanceNumerator / (float)chanceDenominator;
			float inverseChance = 1f - chance;

			float finalChance = chance; //First roll could be a success
			finalChance += inverseChance * rerollChance * chance; //Other success is when first roll fails, reroll happens, and it rolls a success

			float dropRate = finalChance * ratesInfo.parentDroprateChance;
			drops.Add(new DropRateInfo(itemId, amountDroppedMinimum, amountDroppedMaximum, dropRate, ratesInfo.conditions));
			Chains.ReportDroprates(base.ChainedRules, finalChance, drops, ratesInfo);
		}
	}
}
