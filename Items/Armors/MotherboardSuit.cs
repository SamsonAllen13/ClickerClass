using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class MotherboardSuit : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 30000;
			Item.rare = 3;
			Item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage<ClickerDamage>() += 0.06f;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddRecipeGroup("ClickerClass:SilverBar", 25).AddIngredient(ItemID.Wire, 75).AddTile(TileID.Anvils).Register();
		}
	}
}
