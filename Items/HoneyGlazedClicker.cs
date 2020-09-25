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
	public class HoneyGlazedClicker : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Honey Glazed Clicker");
			Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
		}
		
		public override void SetDefaults()
		{
			isClicker = true;
			radiusBoost = 2.45f;
			clickerColorItem = new Color(255, 175, 0, 0);
			clickerDustColor = 153;
			itemClickerAmount = 1;
			itemClickerEffect = "Sticky Honey";
			itemClickerColorEffect = "fcd22c";
			
			item.damage = 13;
			item.width = 30;
			item.height = 30;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = 5;
			item.holdStyle = 3;
			item.knockBack = 2f;
			item.noMelee = true;
			item.value = 1000;
			item.rare = 3;
			item.shoot = mod.ProjectileType("HoneyGlazedClickerPro");
			item.shootSpeed = 1f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BeeWax, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
