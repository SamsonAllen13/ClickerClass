using Terraria;
using Terraria.ID;

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
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Green;
		}

		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().accButtonMasher = true;
		}
	}
}
