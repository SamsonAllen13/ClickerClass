using Terraria;
using Terraria.ID;

namespace ClickerClass.Items.Accessories
{
	public class BottomlessBoxofPaperclips : ClickerItem
	{
		public static readonly int ChargeMax = 100;

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
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.LightPurple;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.accPaperclipsItem = Item;
		}
	}
}
