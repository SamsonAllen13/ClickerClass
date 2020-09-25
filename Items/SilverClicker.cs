using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ObjectData;

namespace ClickerClass.Items
{
	public class SilverClicker : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Silver Clicker");
			Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
		}
		
		public override void SetDefaults()
		{
			isClicker = true;
			radiusBoost = 1.4f;
			clickerColorItem = new Color(200, 225, 225, 0);
			clickerDustColor = 11;
			itemClickerAmount = 8;
			itemClickerEffect = "Double Click";
			itemClickerColorEffect = "ffffff";
			
			item.damage = 6;
			item.width = 30;
			item.height = 30;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = 5;
			item.holdStyle = 3;
			item.knockBack = 1f;
			item.noMelee = true;
			item.value = 1000;
			item.rare = 0;
			item.shoot = mod.ProjectileType("ClickDamage");
			item.shootSpeed = 1f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SilverBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
