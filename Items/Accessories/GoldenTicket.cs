using Terraria;

namespace ClickerClass.Items.Accessories
{
	public class GoldenTicket : ClickerItem
	{
		public override void SetDefaults()
		{
			SetDisplayMoneyGenerated(item);
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 50000;
			item.rare = 4;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerRadius += 0.2f;
			clickerPlayer.accGoldenTicket = true;
		}
	}
}
