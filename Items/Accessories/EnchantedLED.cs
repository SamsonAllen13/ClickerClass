using Terraria;

namespace ClickerClass.Items.Accessories
{
	public class EnchantedLED : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Enchanted LED");
			Tooltip.SetDefault("Your clicks produce an enchanted burst of light, while accessory is visible"
							+ "\nIncreases click damage by 2");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 25000;
			item.rare = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			if (!hideVisual)
			{
				clickerPlayer.accEnchantedLED = true;
			}
			clickerPlayer.clickerDamageFlat += 2;
		}
	}
}
