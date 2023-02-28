using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace ClickerClass.Items.Accessories
{
	public class MousePad : ClickerItem
	{
		public static readonly float RadiusIncrease = 0.25f;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RadiusIncrease * 100f);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 0, 80, 0);
			Item.rare = ItemRarityID.Green;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().clickerRadius += 2 * RadiusIncrease;
		}
	}
}
