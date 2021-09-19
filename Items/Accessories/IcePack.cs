using Terraria;

namespace ClickerClass.Items.Accessories
{
	public class IcePack : ClickerItem
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
			Item.value = 10000;
			Item.rare = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accIcePack = true;
		}
		
		//Generated in Ice Chests
	}
}
