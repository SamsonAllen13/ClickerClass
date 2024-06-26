using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using ClickerClass.Buffs;

namespace ClickerClass.Items.Placeable
{
	public class DesktopComputer : ModItem
	{
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DesktopComputerBuff.ClickAmountDecrease);

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.DesktopComputerTile>());
			Item.width = 26;
			Item.height = 22;
			Item.maxStack = 9999;
			Item.value = Item.sellPrice(0, 3, 0, 0);
			Item.rare = ItemRarityID.LightPurple;
		}
	}
}
