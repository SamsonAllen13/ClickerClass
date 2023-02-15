using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Placeable.Mice
{
	public class MicePlatform : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 200;
		}

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Mice.MicePlatform>());
		}

		public override void AddRecipes()
		{
			CreateRecipe(2).AddIngredient(ModContent.ItemType<MiceBrick>(), 1).Register();
		}
	}
}