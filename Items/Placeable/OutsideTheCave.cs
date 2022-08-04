using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ClickerClass.Tiles;

namespace ClickerClass.Items.Placeable
{
	public class OutsideTheCave : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
		}
		
		public override void SetDefaults() 
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<PaintingLarge>(), 0); //tile, style
			Item.width = 30;
			Item.height = 30;
			Item.maxStack = 99;
			Item.value = 10000;
			Item.rare = ItemRarityID.White;
		}
	}
}
