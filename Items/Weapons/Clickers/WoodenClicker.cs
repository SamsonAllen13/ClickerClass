using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class WoodenClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 0.85f);
			SetColor(Item, new Color(125, 100, 75));
			SetDust(Item, 0);

			Item.damage = 2;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 0.5f;
			Item.value = 24;
			Item.rare = 0;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.Wood, 10).AddTile(TileID.WorkBenches).Register();
		}
	}
}
