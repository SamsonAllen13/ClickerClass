using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	public class StickyKeychain : ClickerItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Every 10 clicks sticks damaging slime on to your screen");
		}

		public override void SetDefaults() 
		{
			isClicker = true;
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
