using Terraria;

namespace ClickerClass.Items.Accessories
{
	public class StickyKeychain : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Tooltip.SetDefault("Every 10 clicks sticks damaging slime on to your screen");
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
			player.GetModPlayer<ClickerPlayer>().clickerStickyAcc = true;
		}
	}
}
