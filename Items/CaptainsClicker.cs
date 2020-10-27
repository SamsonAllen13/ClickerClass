using Microsoft.Xna.Framework;

namespace ClickerClass.Items
{
	public class CaptainsClicker : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Captain's Clicker");
			Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			isClickerWeapon = true;
			radiusBoost = 3.15f;
			clickerColorItem = new Color(255, 225, 50, 0);
			clickerDustColor = 10;
			itemClickerAmount = 12;
			itemClickerEffect = "Bombard";
			itemClickerColorEffect = "fde224";

			item.damage = 30;
			item.width = 30;
			item.height = 30;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = 5;
			item.holdStyle = 3;
			item.knockBack = 1f;
			item.noMelee = true;
			item.value = 180000;
			item.rare = 4;
			item.shoot = mod.ProjectileType("ClickDamage");
			item.shootSpeed = 1f;
		}
	}
}
