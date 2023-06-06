using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class MotherboardSuit : ClickerItem
	{
		public static readonly int DamageIncrease = 6;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageIncrease);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 0, 60, 0);
			Item.rare = ItemRarityID.Orange;
			Item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage<ClickerDamage>() += DamageIncrease / 100f;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddRecipeGroup(ClickerRecipes.AnySilverBarGroup, 25).AddIngredient(ItemID.Wire, 75).AddTile(TileID.Anvils).Register();
		}
	}
}
