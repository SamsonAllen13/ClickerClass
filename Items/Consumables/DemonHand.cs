using Terraria;
using Terraria.ID;
using ClickerClass.Core.Handlers.ClickerDamageDropHandler;
using ClickerClass.DropRules.DropConditions;

namespace ClickerClass.Items.Consumables
{
	public class DemonHand : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickerNPCDropGlobalNPC.AddDrop(NPCID.WallofFlesh, new ClickerNPCDropData(Type, new DemonHandCondition()));
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.useTime = 45;
			Item.useAnimation = 45;
			Item.UseSound = SoundID.Item4;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.value = Item.sellPrice(0, 0, 40, 0);
			Item.rare = ItemRarityID.Expert;
			Item.expert = true;
		}

		public override bool CanUseItem(Player player)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			return !clickerPlayer.consumedDemonHand;
		}

		public override bool? UseItem(Player player)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			if (!clickerPlayer.consumedDemonHand)
			{
				//Needs to run on all sides, for drop code on server
				clickerPlayer.consumedDemonHand = true;
				return true;
			}

			return null;
		}
	}
}
