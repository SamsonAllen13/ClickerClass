using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	public class AncientClickingGlove : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("While in combat, automatically clicks your current clicker every second");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 35000;
			item.rare = 4;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().clickerGloveAcc2 = true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "ClickingGlove", 1);
			recipe.AddIngredient(ItemID.AncientCloth, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
