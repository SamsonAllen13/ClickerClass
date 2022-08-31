using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Placeable.Mice
{
	public class MiceCandle : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
		}

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Mice.MiceCandle>());
			Item.width = 8;
			Item.height = 18;
			Item.maxStack = 99;
			Item.value = Item.sellPrice(0, 0, 0, 60);
			Item.rare = ItemRarityID.White;
			Item.noWet = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceBrick>(), 4).AddIngredient(ItemID.Torch, 1).AddTile(TileID.LunarCraftingStation).Register();
		}
	}
}