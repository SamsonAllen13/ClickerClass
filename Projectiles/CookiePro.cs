using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Projectiles
{
	public class CookiePro : ClickerProjectile
	{
		public Vector2 location = Vector2.Zero;
		
		public int Frame
		{
			get => (int)projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		public bool LockedLocation
		{
			get => projectile.ai[1] == 1f;
			set => projectile.ai[1] = value ? 1f : 0f;
		}

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
			float time = projectile.timeLeft;
			if (projectile.timeLeft > 150)
			{
				time = 300 - projectile.timeLeft;
			}
			return new Color(255, 255, 255, 200) * 0.005f * time;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			projectile.frame = Frame;
			projectile.rotation -= 0.01f;

			if (!LockedLocation)
			{
				location = player.Center - projectile.Center;
				LockedLocation = true;
			}

			projectile.Center = player.Center - location;
			projectile.gfxOffY = player.gfxOffY;
		}
	}
}
