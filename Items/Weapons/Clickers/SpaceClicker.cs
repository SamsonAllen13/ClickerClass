using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SpaceClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Space Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Космический курсор");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.25f);
			SetColor(item, new Color(175, 125, 125, 0));
			SetDust(item, 174);
			SetAmount(item, 8);
			SetEffect(item, "Star Storm");


			item.damage = 10;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 15000;
			item.rare = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MeteoriteBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
