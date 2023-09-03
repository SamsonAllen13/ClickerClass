using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ClickerClass.Items.Misc;

namespace ClickerClass.Items.Placeable
{
	public class DungeonChest : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.DungeonChest>());
			Item.width = 26;
			Item.height = 22;
			Item.maxStack = 9999;
			Item.value = Item.sellPrice(0, 0, 5, 0);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(10);
			recipe.AddIngredient(ModContent.ItemType<DungeonKey>(), 1);
			recipe.AddIngredient(ItemID.Chest, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
