using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class TitaniumClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Titanium Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Титановый курсор");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 3.25f);
			SetColor(item, new Color(150, 150, 150, 0));
			SetDust(item, 146);
			SetAmount(item, 12);
			SetEffect(item, "Razor's Edge");


			item.damage = 44;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 161000;
			item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TitaniumBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
