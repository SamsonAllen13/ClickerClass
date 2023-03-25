using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace ClickerClass.Items.Accessories
{
	public class EnchantedLED : ClickerItem
	{
		public static readonly int DamageIncrease = 2;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageIncrease);

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
			Item.rare = ItemRarityID.Blue;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			if (!hideVisual)
			{
				clickerPlayer.accEnchantedLED = true;
			}

			player.GetDamage<ClickerDamage>().Flat += DamageIncrease;
		}
	}
}
