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
	public class TheClicker : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Clicker");
			Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
		}
		
		public override void SetDefaults()
		{
			isClicker = true;
			radiusBoost = 6f;
			clickerColorItem = new Color(255, 255, 255, 0);
			clickerDustColor = 91;
			itemClickerAmount = 1;
			itemClickerEffect = "The Click";
			itemClickerColorEffect = "ffffff";
			
			item.damage = 150;
			item.width = 30;
			item.height = 30;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = 5;
			item.holdStyle = 3;
			item.knockBack = 1f;
			item.noMelee = true;
			item.value = 1000;
			item.rare = 10;
			item.shoot = mod.ProjectileType("TheClickerPro");
			item.shootSpeed = 1f;
		}
	}
}
