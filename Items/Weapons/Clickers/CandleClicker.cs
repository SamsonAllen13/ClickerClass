using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CandleClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Candle Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Курсор-свеча");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 1.5f);
			SetColor(item, new Color(255, 175, 75, 0));
			SetDust(item, 55);
			SetAmount(item, 10);
			SetEffect(item, "Illuminate");


			item.damage = 8;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 25000;
			item.rare = 2;
		}
	}
}
