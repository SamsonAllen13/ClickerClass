using Terraria;

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
			Item.value = 50000;
			Item.rare = 3;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().SetAutoReuseEffect(autoReuseEffect);
		}
	}
}
