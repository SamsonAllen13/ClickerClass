using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class SFXButtonH : SFXButtonBase
	{
		public override void UpdateInventory(Player player) 
		{
			player.GetModPlayer<ClickerPlayer>().accSFXButtonH += Item.stack;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accSFXButtonH += Item.stack;
		}
	}
}
