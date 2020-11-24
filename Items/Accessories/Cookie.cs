using Terraria;

namespace ClickerClass.Items.Accessories
{
	public class Cookie : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Tooltip.SetDefault("While equipped, cookies will periodically spawn within your clicker radius"
							+ "\nClick the cookie to gain bonus clicker damage, radius, and life regeneration");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 20000;
			item.rare = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accCookie = true;
		}
	}
}
