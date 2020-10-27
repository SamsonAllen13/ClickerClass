using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	public class SpectreClicker : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spectre Clicker");
			Tooltip.SetDefault("Click on an enemy within sight to damage them");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			isClickerWeapon = true;
			radiusBoost = 5f;
			clickerColorItem = new Color(100, 255, 255, 0);
			clickerDustColor = 88;
			itemClickerAmount = 1;
			itemClickerEffect = "Phase Reach";
			itemClickerColorEffect = "71ffff";

			item.damage = 50;
			item.width = 30;
			item.height = 30;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = 5;
			item.holdStyle = 3;
			item.knockBack = 1f;
			item.noMelee = true;
			item.value = 450000;
			item.rare = 8;
			item.shoot = mod.ProjectileType("ClickDamage");
			item.shootSpeed = 1f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpectreBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
