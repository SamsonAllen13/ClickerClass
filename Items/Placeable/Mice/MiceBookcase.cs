using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Placeable.Mice
{
	public class MiceBookcase : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Mice.MiceBookcase>());
			Item.width = 20;
			Item.height = 20;
			Item.value = Item.sellPrice(0, 0, 0, 60);
			Item.rare = ItemRarityID.White;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceBrick>(), 20).AddIngredient(ItemID.Book, 10).AddTile(TileID.LunarCraftingStation).Register();
		}
	}
}