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
			SetRadius(item, 1.4f);
			SetColor(item, new Color(200, 225, 225));
			SetDust(item, 11);
			AddEffect(item, ClickEffect.DoubleClick2);


			item.damage = 6;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 4500;
			item.rare = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SilverBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
