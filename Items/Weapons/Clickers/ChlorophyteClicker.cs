using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class ChlorophyteClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Chlorophyte Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 3.75f);
			SetColor(item, new Color(175, 255, 100, 0));
			SetDust(item, 89);
			SetAmount(item, 10);
			SetEffect(item, "Toxic Release");


			item.damage = 54;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 250000;
			item.rare = 7;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
