using ClickerClass.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Placeable
{
	public class Galaxies : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Painting6x4>(), 0); //tile, style
			Item.width = 30;
			Item.height = 30;
			Item.value = Item.sellPrice(0, 0, 20, 0);
			Item.rare = ItemRarityID.White;
		}
	}
}
