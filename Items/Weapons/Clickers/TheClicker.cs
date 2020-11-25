using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class TheClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("The Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Курсор");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 6f);
			SetColor(item, new Color(255, 255, 255, 0));
			SetDust(item, 91);
			SetAmount(item, 1);
			SetEffect(item, "The Click");


			item.damage = 150;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 10;
		}
	}
}
