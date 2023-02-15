using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Placeable.Mice
{
	public class MiceClock : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Mice.MiceClock>());
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 99;
			Item.value = Item.sellPrice(0, 0, 0, 60);
			Item.rare = ItemRarityID.White;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceBrick>(), 10).AddRecipeGroup(RecipeGroupID.IronBar, 3).AddIngredient(ItemID.Glass, 6).AddTile(TileID.LunarCraftingStation).Register();
		}
	}
}