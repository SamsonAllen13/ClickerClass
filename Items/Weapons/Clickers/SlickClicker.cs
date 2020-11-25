using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SlickClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Slick Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Скользкий курсор");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.45f);
			SetColor(item, new Color(75, 75, 255, 0));
			SetDust(item, 33);
			SetAmount(item, 6);
			SetEffect(item, "Splash");


			item.damage = 11;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 87500;
			item.rare = 2;
		}
	}
}
