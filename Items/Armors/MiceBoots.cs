using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class MiceBoots : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mice Boots");
			Tooltip.SetDefault("Increases click damage by 6%"
							+ "\nIncreases movement speed by 20%");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Сапоги фрагмента мышки");
			Tooltip.AddTranslation(GameCulture.Russian, "Увеличивает урон кликов на 6%\nУвеличивает скорость передвижения на 20%");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 105000;
			item.rare = 10;
			item.defense = 24;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.06f;
			player.moveSpeed += 0.20f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "MiceFragment", 15);
			recipe.AddIngredient(ItemID.LunarBar, 12);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}