using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	[AutoloadEquip(EquipType.Waist)]
	public class GamerCrate : ClickerItem
	{
		public static readonly int RadiusIncrease = 50;
		public static readonly int DamageIncrease = 10;
		public static readonly int ClickAmountDecrease = 20;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageIncrease, RadiusIncrease, ClickAmountDecrease);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			SetDisplayTotalClicks(Item);
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Lime;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerRadius += 2 * RadiusIncrease / 100f;
			player.GetDamage<ClickerDamage>() += DamageIncrease / 100f;
			clickerPlayer.clickerBonusPercent -= ClickAmountDecrease / 100f;
			clickerPlayer.SetAutoReuseEffect(HandCream.autoReuseEffect);
			if (!hideVisual)
			{
				clickerPlayer.accEnchantedLED2 = true;
			}
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<EnchantedLED>(), 1).AddIngredient(ModContent.ItemType<Soda>(), 1).AddIngredient(ModContent.ItemType<MousePad>(), 1).AddIngredient(ModContent.ItemType<HandCream>(), 1).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
