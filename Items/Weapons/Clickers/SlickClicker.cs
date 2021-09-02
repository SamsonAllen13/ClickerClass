using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SlickClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Splash = ClickerSystem.RegisterClickEffect(mod, "Splash", null, null, 6, new Color(75, 75, 255), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < 6; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-6f, -2f), ModContent.ProjectileType<SlickClickerPro>(), (int)(damage * 0.75f), knockBack, player.whoAmI, hasSpawnEffects);
					spawnEffects = false;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.45f);
			SetColor(item, new Color(75, 75, 255));
			SetDust(item, 33);
			AddEffect(item, ClickEffect.Splash);

			item.damage = 10;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 87500;
			item.rare = 2;
		}
	}
}
