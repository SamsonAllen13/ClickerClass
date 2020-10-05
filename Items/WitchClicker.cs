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
	public class WitchClicker : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Witch Clicker");
			Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
		}
		
		public override void SetDefaults()
		{
			isClicker = true;
			radiusBoost = 6f;
			clickerColorItem = new Color(175, 75, 255, 0);
			clickerDustColor = 173;
			itemClickerAmount = 6;
			itemClickerEffect = "Wild Magic";
			itemClickerColorEffect = "ac59ff";
			
			item.damage = 78;
			item.width = 30;
			item.height = 30;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = 5;
			item.holdStyle = 3;
			item.knockBack = 1f;
			item.noMelee = true;
			item.value = 500000;
			item.rare = 8;
			item.shoot = mod.ProjectileType("ClickDamage");
			item.shootSpeed = 1f;
		}
		
		public override bool CanUseItem(Player player)
		{
			if (player.HasBuff(ModContent.BuffType<AutoClick>()))
			{
				item.autoReuse = true;
				item.useTime = 4;
				item.useAnimation = 4;
			}
			else
			{
				item.autoReuse = false;
				item.useTime = 1;
				item.useAnimation = 1;
			}
			return base.CanUseItem(player);
		}
	}
}
