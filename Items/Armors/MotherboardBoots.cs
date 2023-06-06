using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class MotherboardBoots : ClickerItem
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
			Item.value = Item.sellPrice(0, 0, 50, 0);
			Item.rare = ItemRarityID.Orange;
			Item.defense = 7;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage<ClickerDamage>() += DamageIncrease / 100f;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddRecipeGroup(ClickerRecipes.AnySilverBarGroup, 20).AddIngredient(ItemID.Wire, 50).AddTile(TileID.Anvils).Register();
		}
	}
}
