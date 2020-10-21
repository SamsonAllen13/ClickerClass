using Terraria;

namespace ClickerClass.Items
{
	public class Milk : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glass of Milk");
			Tooltip.SetDefault("Gain up to 15% clicker damage based on your amount of clicks within a second");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 10000;
			item.rare = 2;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().clickerMilkAcc = true;
		}
	}
}
