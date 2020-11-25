using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class TungstenClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Tungsten Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Вольфрамовый курсор");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 1.45f);
			SetColor(item, new Color(125, 175, 150, 0));
			SetDust(item, 83);
			SetAmount(item, 8);
			SetEffect(item, "Double Click");


			item.damage = 6;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 6750;
			item.rare = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TungstenBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
