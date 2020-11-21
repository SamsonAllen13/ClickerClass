using Microsoft.Xna.Framework;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class EclipticClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Ecliptic Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 3.5f);
			SetColor(item, new Color(255, 200, 100, 0));
			SetDust(item, 264);
			SetAmount(item, 15);
			SetEffect(item, "Totality");


			item.damage = 48;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 165000;
			item.rare = 6;
		}
	}
}
