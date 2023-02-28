using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace ClickerClass.Items.Accessories
{
	public class MouseTrap : ClickerItem
	{
		public static readonly int DamageIncrease = 15;
		public static readonly int ClickAmountDecrease = 1;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageIncrease, ClickAmountDecrease);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Green;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			player.GetDamage<ClickerDamage>() += DamageIncrease / 100f;
			clickerPlayer.clickerBonusPercent -= ClickAmountDecrease / 100f;
			clickerPlayer.accMouseTrap = true;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe(1).AddCondition(Recipe.Condition.InGraveyardBiome).AddRecipeGroup("IronBar", 8).AddRecipeGroup("Wood", 6).AddIngredient(ItemID.ShadowScale, 6).AddTile(TileID.Anvils).Register();
			
			CreateRecipe(1).AddCondition(Recipe.Condition.InGraveyardBiome).AddRecipeGroup("IronBar", 8).AddRecipeGroup("Wood", 6).AddIngredient(ItemID.TissueSample, 6).AddTile(TileID.Anvils).Register();
		}
	}
}
