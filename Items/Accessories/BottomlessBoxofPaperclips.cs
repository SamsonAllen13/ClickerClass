using Terraria;

namespace ClickerClass.Items.Accessories
{
	public class BottomlessBoxofPaperclips : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		//Any Mechnical Boss drop+bag
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = 50000;
			Item.rare = 6;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.accPaperclipsItem = Item;
		}
	}
}
