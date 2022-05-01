using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Projectiles
{
	public class CookiePro : ClickerProjectile
	{
		public Vector2 location = Vector2.Zero;
		
		public int Frame
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public bool LockedLocation
		{
			get => Projectile.ai[1] == 1f;
			set => Projectile.ai[1] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			float time = Projectile.timeLeft;
			if (Projectile.timeLeft > 150)
			{
				time = 300 - Projectile.timeLeft;
			}
			return new Color(255, 255, 255, 200) * 0.005f * time * Projectile.Opacity;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			Projectile.frame = Frame;
			Projectile.rotation -= 0.01f;

			if (player.whoAmI != Projectile.owner)
			{
				//Hide for everyone but the owner
				Projectile.alpha = 255;
			}

			if (!LockedLocation)
			{
				location = player.Center - Projectile.Center;
				LockedLocation = true;
			}

			Projectile.Center = player.Center - location;
			Projectile.gfxOffY = player.gfxOffY;
		}
	}
}
