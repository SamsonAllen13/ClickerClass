using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class HighTechClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("High Tech Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Высокотехнологичный курсор");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 6f);
			SetColor(item, new Color(75, 255, 200, 0));
			SetDust(item, 226);
			SetAmount(item, 10);
			SetEffect(item, "Discharge");


			item.damage = 90;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 500000;
			item.rare = 8;
		}
	}
}
