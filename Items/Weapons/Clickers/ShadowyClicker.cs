using Microsoft.Xna.Framework;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class ShadowyClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Shadowy Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.15f);
			SetColor(item, new Color(150, 100, 255, 0));
			SetDust(item, 27);
			SetAmount(item, 12);
			SetEffect(item, "Curse");


			item.damage = 12;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 15000;
			item.rare = 1;
		}
	}
}
