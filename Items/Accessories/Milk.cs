﻿using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	[AutoloadEquip(EquipType.Waist)]
	public class Milk : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Glass of Milk");
			Tooltip.SetDefault("Gain up to 15% clicker damage based on your amount of clicks within a second");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Стакан молока");
			Tooltip.AddTranslation(GameCulture.Russian, "Увеличивает урон кликов до 15% в зависимости от количества совершённых кликов в секунду");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 10000;
			item.rare = 2;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accGlassOfMilk = true;
		}
	}
}
