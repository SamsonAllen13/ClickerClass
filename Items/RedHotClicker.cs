using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	public class RedHotClicker : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Red Hot Clicker");
			Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			isClickerWeapon = true;
			isClickerWeapon = true;
			radiusBoost = 2.6f;
			clickerColorItem = new Color(255, 125, 0, 0);
			clickerDustColor = 174;
			itemClickerAmount = 8;
			itemClickerEffect = "Inferno";
			itemClickerColorEffect = "ec7e06";

			item.damage = 17;
			item.width = 30;
			item.height = 30;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = 5;
			item.holdStyle = 3;
			item.knockBack = 2f;
			item.noMelee = true;
			item.value = 27000;
			item.rare = 3;
			item.shoot = mod.ProjectileType("ClickDamage");
			item.shootSpeed = 1f;
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
