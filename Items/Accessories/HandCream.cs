using Terraria;
using Terraria.ID;

namespace ClickerClass.Items.Accessories
{
	public class HandCream : ClickerItem
	{
		public static readonly AutoReuseEffect autoReuseEffect = new(12f, ControlledByKeyBind: true);

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
			Item.rare = ItemRarityID.Orange;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().SetAutoReuseEffect(autoReuseEffect);
		}
	}
}
