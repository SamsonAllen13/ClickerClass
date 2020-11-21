using Terraria;

namespace ClickerClass.Items.Accessories
{
	public class ChocolateChip : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Tooltip.SetDefault("Every 15 clicks releases a burst of damaging chocolate");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 50000;
			item.rare = 4;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().clickerChocolateChipAcc = true;
		}
	}
}
