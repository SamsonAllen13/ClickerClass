using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ObjectData;
using ClickerClass.Buffs;

namespace ClickerClass.Items
{
	public class FrozenClicker : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frozen Clicker");
			Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
		}
		
		public override void SetDefaults()
		{
			isClicker = true;
			radiusBoost = 6f;
			clickerColorItem = new Color(175, 255, 255, 0);
			clickerDustColor = 15;
			itemClickerAmount = 1;
			itemClickerEffect = "Freeze";
			itemClickerColorEffect = "aaffff";
			
			item.damage = 82;
			item.width = 30;
			item.height = 30;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = 5;
			item.holdStyle = 3;
			item.knockBack = 1f;
			item.noMelee = true;
			item.value = 1000;
			item.rare = 8;
			item.shoot = mod.ProjectileType("FrozenClickerPro");
			item.shootSpeed = 1f;
		}
	}
}
