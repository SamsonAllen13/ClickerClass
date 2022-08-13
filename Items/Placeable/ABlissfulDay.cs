using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ClickerClass.Tiles;

namespace ClickerClass.Items.Placeable
{
	public class ABlissfulDay : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
		}
		
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<PaintingMedium>(), 0); //tile, style
			Item.width = 30;
			Item.height = 30;
			Item.maxStack = 99;
			Item.value = Item.sellPrice(0, 0, 20, 0);
			Item.rare = ItemRarityID.White;
		}
	}
}
