using Terraria;
using Terraria.ID;

namespace ClickerClass.Items.Accessories
{
	public class GoldenTicket : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			SetDisplayMoneyGenerated(Item);
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.LightRed;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerRadius += 0.2f;
			clickerPlayer.accGoldenTicket = true;
		}
	}
}
