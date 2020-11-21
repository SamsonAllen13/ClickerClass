using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Projectiles
{
	public class CookiePro : ClickerProjectile
	{
		public Vector2 location = Vector2.Zero;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			if (projectile.timeLeft > 150)
			{
				return new Color(255, 255, 255, 200) * (0.005f * (int)(300 - projectile.timeLeft));
			}
			else
			{
				return new Color(255, 255, 255, 200) * (0.005f * projectile.timeLeft);
			}
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			projectile.frame = (int)projectile.ai[0];
			projectile.rotation -= 0.01f;

			if (projectile.ai[1] < 1f)
			{
				location = player.Center - projectile.Center;
				projectile.ai[1] += 1f;
			}

			projectile.Center = new Vector2(player.Center.X, player.Center.Y + player.gfxOffY) - location;
		}
	}
}