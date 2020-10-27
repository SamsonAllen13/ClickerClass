using Microsoft.Xna.Framework;

namespace ClickerClass.Items
{
	public class HemoClicker : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hemo Clicker");
			Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			isClickerWeapon = true;
			radiusBoost = 1.75f;
			clickerColorItem = new Color(255, 50, 50, 0);
			clickerDustColor = 5;
			itemClickerAmount = 10;
			itemClickerEffect = "Linger";
			itemClickerColorEffect = "ff6a6a";

			item.damage = 6;
			item.width = 30;
			item.height = 30;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = 5;
			item.holdStyle = 3;
			item.knockBack = 2f;
			item.noMelee = true;
			item.value = 13000;
			item.rare = 1;
			item.shoot = mod.ProjectileType("ClickDamage");
			item.shootSpeed = 1f;
		}
	}
}
