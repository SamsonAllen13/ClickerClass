using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class MilkCookies : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = 65000;
			Item.rare = 3;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.accCookieItem = Item;
			clickerPlayer.accCookie2 = true;
			clickerPlayer.accGlassOfMilk = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Cookie>(), 1).AddIngredient(ModContent.ItemType<Milk>(), 1).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
