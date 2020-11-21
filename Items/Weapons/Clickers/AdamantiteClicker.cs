using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class AdamantiteClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Adamantite Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			SetRadius(item, 3.15f);
			SetColor(item, new Color(255, 25, 25, 0));
			SetDust(item, 50);
			SetAmount(item, 10);
			SetEffect(item, "True Strike");

			item.damage = 32;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 138000;
			item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.AdamantiteBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
