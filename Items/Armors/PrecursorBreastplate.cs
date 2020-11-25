using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class PrecursorBreastplate : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Precursor Breastplate");
			Tooltip.SetDefault("Increases click damage by 10%"
							+ "\nReduces base clicker radius by 50%");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Нагрудник предтечей");
			Tooltip.AddTranslation(GameCulture.Russian, "Увеличивает урон от кликов на 10%\nУменьшает базовый радиус курсора на 50%");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 80000;
			item.rare = 8;
			item.defense = 22;
		}

		public override void UpdateEquip(Player player)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerDamage += 0.1f;
			clickerPlayer.clickerRadius -= 1f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarTabletFragment, 18);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}