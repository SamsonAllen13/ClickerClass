using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Tools
{
	public class MicePickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mice Pickaxe");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Кирка фрагмента мышки");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.useTime = 6;
			item.useAnimation = 12;
			item.damage = 80;
			item.melee = true;
			item.pick = 225;
			item.useStyle = 1;
			item.knockBack = 5.5f;
			item.tileBoost = 4;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 10;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "MiceFragment", 12);
			recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}