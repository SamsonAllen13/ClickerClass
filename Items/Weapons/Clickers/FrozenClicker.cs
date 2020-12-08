using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class FrozenClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Freeze = ClickerSystem.RegisterClickEffect(mod, "Freeze", null, null, 1, new Color(175, 255, 255), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<FrozenClickerPro>(), 0, 0f, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 6f);
			SetColor(item, new Color(175, 255, 255));
			SetDust(item, 92);
			AddEffect(item, ClickEffect.Freeze);

			item.damage = 82;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 500000;
			item.rare = 8;
		}
	}
}
