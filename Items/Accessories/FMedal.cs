using Terraria;
using Terraria.ID;

namespace ClickerClass.Items.Accessories
{
	public class FMedal : ClickerItem
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
			Item.value = 50000;
			Item.rare = 4;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.accFMedalItem = Item;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.GoldBar, 5).AddIngredient(ItemID.SoulofNight, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
