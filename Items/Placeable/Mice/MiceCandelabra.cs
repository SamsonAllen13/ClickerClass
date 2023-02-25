using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Placeable.Mice
{
	public class MiceCandelabra : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Mice.MiceCandelabra>());
			Item.width = 20;
			Item.height = 20;
			Item.value = Item.sellPrice(0, 0, 3, 0);
			Item.rare = ItemRarityID.White;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceBrick>(), 5).AddIngredient(ItemID.Torch, 3).AddTile(TileID.LunarCraftingStation).Register();
		}
	}
}