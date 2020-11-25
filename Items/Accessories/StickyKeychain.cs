using Terraria;
using Terraria.Localization;

namespace ClickerClass.Items.Accessories
{
	public class StickyKeychain : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Tooltip.SetDefault("Every 10 clicks sticks damaging slime on to your screen");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Липкая связка ключей");
			Tooltip.AddTranslation(GameCulture.Russian, "Каждые 10 кликов лепит наносящую урон слизь на ваш экран");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 25000;
			item.rare = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accStickyKeychain = true;
		}
	}
}
