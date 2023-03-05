using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Consumables
{
	public class HeavenlyChip : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.useTime = 45;
			Item.useAnimation = 45;
			Item.UseSound = SoundID.Item92;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.value = Item.sellPrice(0, 0, 40, 0);
			Item.rare = ItemRarityID.LightPurple;
		}

		public override bool CanUseItem(Player player)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			return (!clickerPlayer.consumedHeavenlyChip);
		}

		public override bool? UseItem(Player player)
		{
			if (player.itemAnimation > 0 && player.itemTime == 0)
			{
				ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				clickerPlayer.consumedHeavenlyChip = true;
				return true;
			}

			return null;
		}
	}

	// FOR TESTING ONLY
	public class HeavenlyChipReverter : ModItem
	{
		public override string Texture => "ClickerClass/Items/Consumables/HeavenlyChip";

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Item.ResearchUnlockCount = 0;
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.useTime = 45;
			Item.useAnimation = 45;
			Item.UseSound = SoundID.Item92;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.rare = ItemRarityID.Gray;
		}

		public override bool? UseItem(Player player)
		{
			if (player.itemAnimation > 0 && player.itemTime == 0)
			{
				ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				clickerPlayer.consumedHeavenlyChip = false;
				return true;
			}

			return null;
		}
	}
}
