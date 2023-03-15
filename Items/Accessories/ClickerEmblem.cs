using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace ClickerClass.Items.Accessories
{
	public class ClickerEmblem : ClickerItem
	{
		public static readonly int DamageIncrease = 15;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageIncrease);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 2, 0, 0);
			Item.rare = ItemRarityID.LightRed;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage<ClickerDamage>() += DamageIncrease / 100f;
		}
	}
}
