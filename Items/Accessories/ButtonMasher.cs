using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class ButtonMasher : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = 50000;
			Item.rare = 2;
		}
		
		//Sharing to party members handled in ClickerPlayer.PostUpdateEquips
		public override void UpdateInventory(Player player) 
		{
			player.GetModPlayer<ClickerPlayer>().accButtonMasher = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accButtonMasher = true;
		}
	}
}
