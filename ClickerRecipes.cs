using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass
{
	internal class ClickerRecipes
	{
		private static ModRecipe GetNewRecipe()
		{
			return new ModRecipe(ClickerClass.mod);
		}

		public static void AddRecipes()
		{
			ModRecipe recipe = GetNewRecipe();
			recipe.AddIngredient(null, "ClickerEmblem", 1);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(ItemID.AvengerEmblem, 1);
			recipe.AddRecipe();
		}

		public static void AddRecipeGroups()
		{
			RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.SilverBar), new int[]
			{
				ItemID.SilverBar,
				ItemID.TungstenBar,
			});
			RecipeGroup.RegisterGroup("ClickerClass:SilverBar", group);
		}
	}
}
