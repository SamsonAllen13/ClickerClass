using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	public class SpaceClicker : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Space Clicker");
			Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			isClickerWeapon = true;
			radiusBoost = 2.25f;
			clickerColorItem = new Color(175, 125, 125, 0);
			clickerDustColor = 174;
			itemClickerAmount = 8;
			itemClickerEffect = "Star Storm";
			itemClickerColorEffect = "b97171";

			item.damage = 10;
			item.width = 30;
			item.height = 30;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = 5;
			item.holdStyle = 3;
			item.knockBack = 1f;
			item.noMelee = true;
			item.value = 15000;
			item.rare = 1;
			item.shoot = mod.ProjectileType("ClickDamage");
			item.shootSpeed = 1f;
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
