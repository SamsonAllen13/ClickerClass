using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class RedHotClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Red Hot Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Пылающе красный курсор");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.6f);
			SetColor(item, new Color(255, 125, 0, 0));
			SetDust(item, 174);
			SetAmount(item, 8);
			SetEffect(item, "Inferno");


			item.damage = 17;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 27000;
			item.rare = 3;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HellstoneBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
