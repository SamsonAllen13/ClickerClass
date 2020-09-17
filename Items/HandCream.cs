using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	public class HandCream : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Pressing the 'Clicker Accessory' key will toggle auto click on all Clickers"
							+ "\nWhile auto click is enabled, click rates are decreased");
		}

		public override void SetDefaults() 
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 50000;
			item.rare = 3;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) 
		{
			player.GetModPlayer<ClickerPlayer>().clickerAutoClickAcc = true;
		}
	}
}
