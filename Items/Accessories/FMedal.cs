using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace ClickerClass.Items.Accessories
{
	public class FMedal : ClickerItem
	{
		public static readonly int ChargeMeterMax = 200;
		public static readonly int ChargeAmount = 100;
		public static readonly int ChargeMeterStep = 20;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ChargeAmount);

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
			Item.rare = ItemRarityID.LightRed;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.accFMedalItem = Item;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe(1).AddRecipeGroup(ClickerRecipes.AnyGoldBarGroup, 8).AddIngredient(ItemID.SoulofNight, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
