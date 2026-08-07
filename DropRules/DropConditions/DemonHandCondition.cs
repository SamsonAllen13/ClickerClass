using ClickerClass.Core.Handlers.ClickerDamageDropHandler;
using Terraria.GameContent.ItemDropRules;

namespace ClickerClass.DropRules.DropConditions
{
	public class DemonHandCondition : IItemDropRuleCondition
	{
		public bool CanDrop(DropAttemptInfo info)
		{
			if (info.IsInSimulation)
			{
				return false;
			}

			if (!info.npc.TryGetGlobalNPC<ClickerNPCDropGlobalNPC>(out var globalNPC))
			{
				return false;
			}

			return globalNPC.DamagedByPlayer(info.player.whoAmI) && !info.player.GetModPlayer<ClickerPlayer>().consumedDemonHand;
		}

		public bool CanShowItemDropInUI()
		{
			return true;
		}

		public string GetConditionDescription()
		{
			return null;
		}
	}
}
