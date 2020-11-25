using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class LordsClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Lord's Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Курсор Лорда");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 6f);
			SetColor(item, new Color(100, 255, 200, 0));
			SetDust(item, 110);
			SetAmount(item, 15);
			SetEffect(item, "Conqueror");


			item.damage = 122;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 10;
		}
	}
}
