using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	[AutoloadEquip(EquipType.Body)]
	public class MiceSuit : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mice Suit");
			Tooltip.SetDefault("Increases click damage by 10%"
							+ "\nIncreases your base click radius by 25%");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			item.width = 18;
			item.height = 18;
			item.value = 140000;
			item.rare = 10;
			item.defense = 28;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.10f;
			player.GetModPlayer<ClickerPlayer>().clickerRadius += 0.5f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "MiceFragment", 20);
			recipe.AddIngredient(ItemID.LunarBar, 16);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}