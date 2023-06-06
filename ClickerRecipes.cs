using ClickerClass.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass
{
	internal class ClickerRecipes : ModSystem
	{
		public static int AnySilverBarGroup { get; private set; }
		public static int AnyGoldBarGroup { get; private set; }

		public static LocalizedText RecipeGroupAnyText { get; private set; }

		public override void OnModLoad()
		{
			string category = $"RecipeGroups.";
			RecipeGroupAnyText ??= Language.GetOrRegister(Mod.GetLocalizationKey($"{category}Any"));
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemID.AvengerEmblem);
			recipe.AddIngredient(ModContent.ItemType<ClickerEmblem>(), 1);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.Register();
		}

		public override void AddRecipeGroups()
		{
			string any = Language.GetTextValue("LegacyMisc.37");
			RecipeGroup group = new RecipeGroup(() => RecipeGroupAnyText.Format(any, Lang.GetItemNameValue(ItemID.SilverBar)), new int[]
			{
				ItemID.SilverBar,
				ItemID.TungstenBar,
			});
			AnySilverBarGroup = RecipeGroup.RegisterGroup(nameof(ItemID.SilverBar), group);

			group = new RecipeGroup(() => RecipeGroupAnyText.Format(any, Lang.GetItemNameValue(ItemID.GoldBar)), new int[]
			{
				ItemID.GoldBar,
				ItemID.PlatinumBar,
			});
			AnyGoldBarGroup = RecipeGroup.RegisterGroup(nameof(ItemID.GoldBar), group);
		}
	}
}
