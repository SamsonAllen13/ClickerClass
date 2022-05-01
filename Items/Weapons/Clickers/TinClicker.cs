using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class TinClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.15f);
			SetColor(Item, new Color(125, 125, 75));
			SetDust(Item, 81);
			AddEffect(Item, ClickEffect.DoubleClick);

			Item.damage = 3;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 675;
			Item.rare = 0;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.TinBar, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
