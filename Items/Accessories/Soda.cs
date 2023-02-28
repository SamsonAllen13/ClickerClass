﻿using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	[AutoloadEquip(EquipType.Waist)]
	public class Soda : ClickerItem
	{
		public static readonly int ClickAmountDecreaseFlat = 1;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ClickAmountDecreaseFlat);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 0, 50, 0);
			Item.rare = ItemRarityID.Green;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().clickerBonus += ClickAmountDecreaseFlat;
		}
	}
}
