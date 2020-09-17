using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	public class Cookie : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Increases click damage by 2");
		}

		public override void SetDefaults() 
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 10000;
			item.rare = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) 
		{
			player.GetModPlayer<ClickerPlayer>().clickerDamage += 2;
		}
	}
}
