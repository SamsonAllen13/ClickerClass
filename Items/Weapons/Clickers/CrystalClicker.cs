using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CrystalClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Crystal Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Кристальный курсор");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 3.1f);
			SetColor(item, new Color(200, 50, 255, 0));
			SetDust(item, 86);
			SetAmount(item, 8);
			SetEffect(item, "Dazzle");


			item.damage = 34;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 90000;
			item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CrystalShard, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
