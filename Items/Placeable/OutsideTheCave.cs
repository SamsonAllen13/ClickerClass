using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ClickerClass.Tiles;

namespace ClickerClass.Items.Placeable
{
	public class OutsideTheCave : ModItem
	{
		public override void SetDefaults() 
		{
			Item.width = 30;
			Item.height = 30;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.value = 10000;
			Item.rare = ItemRarityID.White;
			Item.createTile = ModContent.TileType<PaintingLarge>();
			Item.placeStyle = 0;
		}
	}
}