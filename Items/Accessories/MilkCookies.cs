﻿using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class MilkCookies : ClickerItem
	{
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Milk.DamageIncrease);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 1, 30, 0);
			Item.rare = ItemRarityID.Orange;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.accCookieItem = Item;
			clickerPlayer.accCookie2 = true;
			clickerPlayer.accGlassOfMilk = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Cookie>(), 1).AddIngredient(ModContent.ItemType<Milk>(), 1).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
