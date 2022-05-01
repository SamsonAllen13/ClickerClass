using ClickerClass.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass
{
	internal class ClickerRecipes : ModSystem
	{
		private static Recipe CreateRecipe(int result, int amount = 1)
		{
			return ClickerClass.mod.CreateRecipe(result, amount);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(ItemID.AvengerEmblem);
			recipe.AddIngredient(ModContent.ItemType<ClickerEmblem>(), 1);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.Register();
		}

		public override void AddRecipeGroups()
		{
			RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.SilverBar), new int[]
			{
				ItemID.SilverBar,
				ItemID.TungstenBar,
			});
			RecipeGroup.RegisterGroup("ClickerClass:SilverBar", group);
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.GoldBar), new int[]
			{
				ItemID.GoldBar,
				ItemID.PlatinumBar,
			});
			RecipeGroup.RegisterGroup("ClickerClass:GoldBar", group);
		}
	}
}
