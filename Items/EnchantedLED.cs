using Terraria;

namespace ClickerClass.Items
{
	public class EnchantedLED : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enchanted LED");
			Tooltip.SetDefault("Your clicks produce an enchanted burst of light"
							+ "\nIncreases click damage by 2");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 25000;
			item.rare = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().clickerEnchantedLED = true;
			player.GetModPlayer<ClickerPlayer>().clickerDamageFlat += 2;
		}
	}
}
