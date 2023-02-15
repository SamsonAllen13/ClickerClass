using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Placeable.Mice
{
	public class MiceBed : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Mice.MiceBed>());
			Item.width = 28;
			Item.height = 20;
			Item.maxStack = 99;
			Item.value = Item.sellPrice(0, 0, 4, 0);
			Item.rare = ItemRarityID.White;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceBrick>(), 15).AddIngredient(ItemID.Silk, 5).AddTile(TileID.LunarCraftingStation).Register();
		}
	}
}