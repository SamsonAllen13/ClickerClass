using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class ChocolateMilkCookies : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Chocolate Milk n' Cookies");
			Tooltip.SetDefault("While equipped, cookies will periodically spawn within your clicker radius"
							+ "\nClick the cookie to gain bonus clicker damage, radius, and life regeneration"
							+ "\nGain up to 15% clicker damage based on your amount of clicks within a second"
							+ "\nEvery 15 clicks releases a burst of damaging chocolate");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Шоколадное молоко с печеньем");
			Tooltip.AddTranslation(GameCulture.Russian, "Будучи надетым, в радиусе вашего курсора будут периодически появляться печенье\nНажмите на печенье, чтобы увеличить урон кликов, радиус курсора и регенерацию здоровья\nУвеличивает урон от кликов до 15% в зависимости от количества совершённых кликов в секунду\nКаждые 15 кликов выпускает скопление наносящего урон шоколада");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 85000;
			item.rare = 5;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.accChocolateChip = true;
			clickerPlayer.accCookie2 = true;
			clickerPlayer.accGlassOfMilk = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "ChocolateChip", 1);
			recipe.AddIngredient(null, "MilkCookies", 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
