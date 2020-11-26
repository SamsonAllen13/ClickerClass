using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Consumables
{
	public class InfluencePotion : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Influence Potion");
			Tooltip.SetDefault("Increases your base click radius by 20%");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Зелье влияния");
			Tooltip.AddTranslation(GameCulture.Russian, "Увеличивает ваш базовый радиус курсора на 20%\nПереведено: [c/e180ff:Project tRU]");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.useTime = 17;
			item.useAnimation = 17;
			item.useStyle = 2;
			item.useTurn = true;
			item.value = Item.sellPrice(0, 0, 2, 0);
			item.consumable = true;
			item.maxStack = 30;
			item.rare = 1;
			item.UseSound = SoundID.Item3;
			item.buffType = mod.BuffType("InfluenceBuff");
			item.buffTime = 18000;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BottledWater, 1);
			recipe.AddIngredient(ItemID.Daybloom, 1);
			recipe.AddIngredient(ItemID.PinkGel, 1);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}