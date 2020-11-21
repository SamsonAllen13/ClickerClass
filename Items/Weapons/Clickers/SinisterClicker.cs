using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SinisterClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Sinister Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.2f);
			SetColor(item, new Color(100, 25, 25, 0));
			SetDust(item, 5);
			SetAmount(item, 10);
			SetEffect(item, "Siphon");


			item.damage = 10;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 18000;
			item.rare = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CrimtaneBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
