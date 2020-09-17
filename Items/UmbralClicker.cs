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
	public class UmbralClicker : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Umbral Clicker");
			Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
		}
		
		public override void SetDefaults()
		{
			isClicker = true;
			radiusBoost = 2.75f;
			clickerColorItem = new Color(150, 100, 255, 0);
			clickerDustColor = 27;
			itemClickerAmount = 10;
			itemClickerEffect = "Shadow Lash";
			itemClickerColorEffect = "7631f7";
			
			item.damage = 20;
			item.width = 30;
			item.height = 30;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = 5;
			item.holdStyle = 3;
			item.knockBack = 2f;
			item.value = 1000;
			item.rare = 3;
			item.shoot = mod.ProjectileType("ClickDamage");
			item.shootSpeed = 1f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DarkClicker", 1);
			recipe.AddIngredient(null, "SlickClicker", 1);
			recipe.AddIngredient(null, "PointyClicker", 1);
			recipe.AddIngredient(null, "RedHotClicker", 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "SinisterClicker", 1);
			recipe.AddIngredient(null, "SlickClicker", 1);
			recipe.AddIngredient(null, "PointyClicker", 1);
			recipe.AddIngredient(null, "RedHotClicker", 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
