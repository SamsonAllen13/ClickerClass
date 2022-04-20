using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ClickerClass.Tiles;

namespace ClickerClass.Items.Placeable
{
	public class ABlissfulDay : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<PaintingMedium>(), 0); //tile, style
			Item.width = 30;
			Item.height = 30;
			Item.maxStack = 99;
			Item.value = 15000;
			Item.rare = ItemRarityID.White;
		}
	}
}
