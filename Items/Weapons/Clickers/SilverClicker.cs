using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SilverClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.4f);
			SetColor(Item, new Color(200, 225, 225));
			SetDust(Item, 11);
			AddEffect(Item, ClickEffect.DoubleClick2);


			Item.damage = 4;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 0.5f;
			Item.value = 4500;
			Item.rare = 0;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.SilverBar, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
