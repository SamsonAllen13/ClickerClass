using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class MotherboardBoots : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Motherboard Boots");
			Tooltip.SetDefault("Increases click damage by 6%");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Сапоги материнской платы");
			Tooltip.AddTranslation(GameCulture.Russian, "Увеличивает урон от кликов на 6%");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 25000;
			item.rare = 3;
			item.defense = 7;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.06f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("ClickerClass:SilverBar", 20);
			recipe.AddIngredient(ItemID.Wire, 50);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}