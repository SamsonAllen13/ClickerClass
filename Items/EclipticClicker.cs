using Microsoft.Xna.Framework;

namespace ClickerClass.Items
{
	public class EclipticClicker : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ecliptic Clicker");
			Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			isClickerWeapon = true;
			radiusBoost = 3.5f;
			clickerColorItem = new Color(255, 200, 100, 0);
			clickerDustColor = 264;
			itemClickerAmount = 15;
			itemClickerEffect = "Totality";
			itemClickerColorEffect = "fdd9a2";

			item.damage = 48;
			item.width = 30;
			item.height = 30;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = 5;
			item.holdStyle = 3;
			item.knockBack = 1f;
			item.noMelee = true;
			item.value = 165000;
			item.rare = 6;
			item.shoot = mod.ProjectileType("ClickDamage");
			item.shootSpeed = 1f;
		}
	}
}
