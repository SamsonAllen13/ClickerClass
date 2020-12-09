using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
namespace ClickerClass.Items.Weapons.Clickers
{
	public class PlatinumClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 1.65f);
			SetColor(item, new Color(125, 150, 175));
			SetDust(item, 11);
			AddEffect(item, ClickEffect.DoubleClick2);

			item.damage = 8;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 13500;
			item.rare = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PlatinumBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
