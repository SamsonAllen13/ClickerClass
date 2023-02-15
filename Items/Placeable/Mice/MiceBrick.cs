using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ClickerClass.Items;

namespace ClickerClass.Items.Placeable.Mice
{
	public class MiceBrick : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 100;
		}

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Mice.MiceBrick>());
		}

		public override void AddRecipes()
		{
			CreateRecipe(10).AddIngredient(ItemID.StoneBlock, 10).AddIngredient(ModContent.ItemType<MiceFragment>(), 1).AddTile(TileID.LunarCraftingStation).Register();

			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceBrickWall>(), 4).AddTile(TileID.WorkBenches).Register();

			CreateRecipe(1).AddIngredient(ModContent.ItemType<MicePlatform>(), 2).Register();
		}
	}
}