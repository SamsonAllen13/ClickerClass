using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class FrozenClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Frozen Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 6f);
			SetColor(item, new Color(175, 255, 255, 0));
			SetDust(item, 15);
			SetAmount(item, 1);
			SetEffect(item, "Freeze");


			item.damage = 82;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 500000;
			item.rare = 8;
			item.shoot = ModContent.ProjectileType<FrozenClickerPro>();
		}
	}
}
