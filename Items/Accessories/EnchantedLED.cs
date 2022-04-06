using Terraria;

namespace ClickerClass.Items.Accessories
{
	public class EnchantedLED : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = 25000;
			Item.rare = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			if (!hideVisual)
			{
				clickerPlayer.accEnchantedLED = true;
			}

			player.GetDamage<ClickerDamage>().Flat += 2;
		}
	}
}
