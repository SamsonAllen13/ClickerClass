using Microsoft.Xna.Framework;

namespace ClickerClass.Items
{
	public class ShadowyClicker : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadowy Clicker");
			Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			radiusBoost = 2.15f;
			clickerColorItem = new Color(150, 100, 255, 0);
			clickerDustColor = 27;
			itemClickerAmount = 12;
			itemClickerEffect = "Curse";
			itemClickerColorEffect = "9f68fb";

			item.damage = 12;
			item.width = 30;
			item.height = 30;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = 5;
			item.holdStyle = 3;
			item.knockBack = 1f;
			item.noMelee = true;
			item.value = 15000;
			item.rare = 1;
			item.shoot = mod.ProjectileType("ClickDamage");
			item.shootSpeed = 1f;
		}
	}
}
