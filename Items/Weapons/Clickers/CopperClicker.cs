using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CopperClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Copper Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Медный курсор");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 1.1f);
			SetColor(item, new Color(255, 150, 75, 0));
			SetDust(item, 9);
			SetAmount(item, 10);
			SetEffect(item, "Double Click");


			item.damage = 4;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 450;
			item.rare = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CopperBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
