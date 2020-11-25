using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class TinClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Tin Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Оловянный курсор");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 1.15f);
			SetColor(item, new Color(125, 125, 75, 0));
			SetDust(item, 81);
			SetAmount(item, 10);
			SetEffect(item, "Double Click");


			item.damage = 4;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 675;
			item.rare = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TinBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
