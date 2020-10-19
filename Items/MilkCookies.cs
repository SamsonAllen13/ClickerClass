using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	public class MilkCookies : ClickerItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Milk n' Cookies");
			Tooltip.SetDefault("While equipped, cookies will periodically spawn within your clicker radius"
							+ "\nClick the cookie to gain bonus clicker damage, radius, and life regeneration"
							+ "\nGain up to 15% clicker damage based on your amount of clicks within a second");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 65000;
			item.rare = 3;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) 
		{
			player.GetModPlayer<ClickerPlayer>().clickerCookieAcc2 = true;
			player.GetModPlayer<ClickerPlayer>().clickerMilkAcc = true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Cookie", 1);
			recipe.AddIngredient(null, "Milk", 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
