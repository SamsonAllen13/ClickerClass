using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	public class MiceHamaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mice Hamaxe");
		}

		public override void SetDefaults()
		{
			item.damage = 60;
			item.melee = true;
			item.width = 20;
			item.height = 20;
			item.useTime = 7;
			item.useAnimation = 28;
			item.tileBoost = 4;
			item.axe = 30;
			item.hammer = 100;
			item.useStyle = 1;
			item.knockBack = 7f;
			item.rare = 10;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "MiceFragment", 14);
			recipe.AddIngredient(ItemID.LunarBar, 12);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}