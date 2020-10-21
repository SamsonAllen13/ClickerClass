using Microsoft.Xna.Framework;

namespace ClickerClass.Items
{
	public class CandleClicker : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Candle Clicker");
			Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			radiusBoost = 1.5f;
			clickerColorItem = new Color(255, 175, 75, 0);
			clickerDustColor = 55;
			itemClickerAmount = 10;
			itemClickerEffect = "Illuminate";
			itemClickerColorEffect = "f5a54b";

			item.damage = 8;
			item.width = 30;
			item.height = 30;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = 5;
			item.holdStyle = 3;
			item.knockBack = 1f;
			item.noMelee = true;
			item.value = 25000;
			item.rare = 2;
			item.shoot = mod.ProjectileType("ClickDamage");
			item.shootSpeed = 1f;
		}
	}
}
