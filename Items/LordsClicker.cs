using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Items
{
	public class LordsClicker : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lord's Clicker");
			Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			isClickerWeapon = true;
			radiusBoost = 6f;
			clickerColorItem = new Color(100, 255, 200, 0);
			clickerDustColor = 110;
			itemClickerAmount = 15;
			itemClickerEffect = "Conqueror";
			itemClickerColorEffect = "adfcdc";

			item.damage = 122;
			item.width = 30;
			item.height = 30;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = 5;
			item.holdStyle = 3;
			item.knockBack = 1f;
			item.noMelee = true;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 10;
			item.shoot = mod.ProjectileType("ClickDamage");
			item.shootSpeed = 1f;
		}
	}
}
