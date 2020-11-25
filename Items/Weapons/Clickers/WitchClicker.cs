using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class WitchClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Witch Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Курсор ведьмы");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 6f);
			SetColor(item, new Color(175, 75, 255, 0));
			SetDust(item, 173);
			SetAmount(item, 6);
			SetEffect(item, "Wild Magic");


			item.damage = 78;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 500000;
			item.rare = 8;
		}
	}
}
