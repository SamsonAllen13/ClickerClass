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
	public class MythrilClicker : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mythril Clicker");
			Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
		}
		
		public override void SetDefaults()
		{
			isClicker = true;
			radiusBoost = 2.95f;
			clickerColorItem = new Color(125, 225, 125, 0);
			clickerDustColor = 49;
			itemClickerAmount = 10;
			itemClickerEffect = "Embrittle";
			itemClickerColorEffect = "79e189";
			
			item.damage = 25;
			item.width = 30;
			item.height = 30;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = 5;
			item.holdStyle = 3;
			item.knockBack = 1f;
			item.noMelee = true;
			item.value = 1000;
			item.rare = 4;
			item.shoot = mod.ProjectileType("ClickDamage");
			item.shootSpeed = 1f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MythrilBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
