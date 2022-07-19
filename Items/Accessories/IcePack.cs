using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	[AutoloadEquip(EquipType.Waist)]
	public class IcePack : ClickerItem
	{
		public static readonly AutoReuseEffect autoReuseEffect = new(16f, ControlledByKeyBind: true);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = 10000;
			Item.rare = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().SetAutoReuseEffect(autoReuseEffect);
		}
	}
}
