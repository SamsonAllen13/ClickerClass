using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CobaltClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Cobalt Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Кобальтовый курсор");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.75f);
			SetColor(item, new Color(50, 125, 200, 0));
			SetDust(item, 48);
			SetAmount(item, 5);
			SetEffect(item, "Haste");


			item.damage = 24;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 45000;
			item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CobaltBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
