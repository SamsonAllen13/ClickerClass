using Terraria;
using Terraria.Localization;

namespace ClickerClass.Items.Accessories
{
	public class ClickerEmblem : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Tooltip.SetDefault("15% increased clicker damage");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Эмблема нажимателя");
			Tooltip.AddTranslation(GameCulture.Russian, "Увеличивает урон кликов на 15%");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 28;
			item.value = 100000;
			item.rare = 4;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.15f;
		}
	}
}
