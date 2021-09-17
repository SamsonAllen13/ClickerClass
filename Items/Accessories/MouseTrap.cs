using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class MouseTrap : ClickerItem
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
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = 2;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			player.GetDamage<ClickerDamage>() += 0.15f;
			clickerPlayer.clickerBonusPercent -= 0.10f;
			clickerPlayer.accMouseTrap = true;
		}
		
		public override void AddRecipes()
		{
			//bool flag6 = !Main.recipe[n].needGraveyardBiome || Main.player[Main.myPlayer].ZoneGraveyard;
			//TODO - Add Graveyard requirement 
			CreateRecipe(1).AddRecipeGroup("IronBar", 8).AddIngredient(ItemID.Wood, 6).AddTile(TileID.Anvils).Register();
		}
	}
}
