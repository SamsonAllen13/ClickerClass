#nullable enable
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.ItemDropRules;

namespace ClickerClass.Core.Handlers.ClickerDamageDropHandler
{
	public class ClickerNPCDropData(List<(int, IItemDropRuleCondition?)> itemTypeCondition)
	{
		//Support a list of (different) item drops
		public List<(int, IItemDropRuleCondition?)> ItemTypeCondition { get; init; } = itemTypeCondition;

		public ClickerNPCDropData(int itemType, IItemDropRuleCondition? dropCondition = null) : this([ (itemType, dropCondition) ])
		{

		}

		public void MergeWith(ClickerNPCDropData other)
		{
			//Add other to this, without duplicates of Item1
			foreach (var tuple in other.ItemTypeCondition)
			{
				if (ItemTypeCondition.All(d => d.Item1 != tuple.Item1))
				{
					ItemTypeCondition.Add(tuple);
				}
			}
		}
	}
}
