using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ClickerClass.Items.Accessories
{
	public class GoldenTicket : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			SetDisplayMoneyGenerated(item);
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 50000;
			item.rare = 4;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().clickerRadius += 0.2f;
			player.GetModPlayer<ClickerPlayer>().accGoldenTicket = true;
		}
	}
}
