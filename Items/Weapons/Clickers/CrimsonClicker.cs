using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CrimsonClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Crimson Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Багровый курсор");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.9f);
			SetColor(item, new Color(255, 225, 175, 0));
			SetDust(item, 87);
			SetAmount(item, 10);
			SetEffect(item, "Infest");


			item.damage = 30;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 105000;
			item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Ichor, 16);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
