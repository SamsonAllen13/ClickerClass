using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	public class GamerCrate : ClickerItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("'You don't actually think someone would play this, do you?'"
						+ "\nIncreases click damage by 10%"
						+ "\nIncreases your base click radius by 50%"
						+ "\nReduces the amount of clicks required for a click effect by 20%"
						+ "\nPressing the 'Clicker Accessory' key will toggle auto click on all Clickers"
						+ "\nWhile auto click is enabled, click rates are decreased");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			isClickerDisplay = true;
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 5);
			item.rare = 7;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) 
		{
			player.GetModPlayer<ClickerPlayer>().clickerRadius += 1f;
			player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.10f;
			player.GetModPlayer<ClickerPlayer>().clickerBonusPercent -= 0.20f;
			player.GetModPlayer<ClickerPlayer>().clickerAutoClickAcc = true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Soda", 1);
			recipe.AddIngredient(null, "Cookie", 1);
			recipe.AddIngredient(null, "MousePad", 1);
			recipe.AddIngredient(null, "HandCream", 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
