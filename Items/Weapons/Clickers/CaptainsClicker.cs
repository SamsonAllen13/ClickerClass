using Microsoft.Xna.Framework;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CaptainsClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Captain's Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 3.15f);
			SetColor(item, new Color(255, 225, 50, 0));
			SetDust(item, 10);
			SetAmount(item, 12);
			SetEffect(item, "Bombard");


			item.damage = 30;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 180000;
			item.rare = 4;
		}
	}
}
