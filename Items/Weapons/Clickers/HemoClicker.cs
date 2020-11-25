using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class HemoClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Hemo Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Кровавый курсор");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 1.75f);
			SetColor(item, new Color(255, 50, 50, 0));
			SetDust(item, 5);
			SetAmount(item, 10);
			SetEffect(item, "Linger");


			item.damage = 6;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 13000;
			item.rare = 1;
		}
	}
}
