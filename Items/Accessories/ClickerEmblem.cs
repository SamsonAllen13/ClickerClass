using Terraria;

namespace ClickerClass.Items.Accessories
{
	public class ClickerEmblem : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.value = 100000;
			Item.rare = 4;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage<ClickerDamage>() += 0.15f;
		}
	}
}
