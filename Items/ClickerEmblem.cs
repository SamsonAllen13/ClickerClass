using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	public class ClickerEmblem : ClickerItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("15% increased clicker damage");
		}

		public override void SetDefaults() 
		{
			isClicker = true;
			item.width = 24;
			item.height = 28;
			item.value = 100000;
			item.rare = 4;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) 
		{
			player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.15f;
		}
	}
}
