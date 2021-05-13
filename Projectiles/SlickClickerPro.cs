using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class SlickClickerPro : ClickerProjectile
	{
		public bool HasSpawnEffects
		{
			get => projectile.ai[0] == 1f;
			set => projectile.ai[0] = value ? 1f : 0f;
		}

		public int GravityTimer
		{
			get => (int)projectile.ai[1];
			set => projectile.ai[1] = value;
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.penetrate = 1;
			projectile.timeLeft = 180;
			projectile.alpha = 255;
			projectile.extraUpdates = 3;
			projectile.ignoreWater = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
		}

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;

				Main.PlaySound(SoundID.NPCHit, (int)projectile.Center.X, (int)projectile.Center.Y, 86);
			}

			if (projectile.timeLeft < 170)
			{
				projectile.friendly = true;
			}

			projectile.velocity.Y /= 1.0065f;

			GravityTimer++;

			if (GravityTimer >= 0)
			{
				projectile.velocity.Y += 1.05f;
				GravityTimer = -15;
			}

			for (int num363 = 0; num363 < 3; num363++)
			{
				float num364 = projectile.velocity.X / 3f * (float)num363;
				float num365 = projectile.velocity.Y / 3f * (float)num363;
				int num366 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 33, 0f, 0f, 100, default(Color), 1.25f);
				Main.dust[num366].position.X = projectile.Center.X - num364;
				Main.dust[num366].position.Y = projectile.Center.Y - num365;
				Main.dust[num366].velocity *= 0f;
				Main.dust[num366].noGravity = true;
			}

			for (int k = 0; k < 2; k++)
			{
				int dust = Dust.NewDust(projectile.position, 10, 10, 33, Main.rand.Next((int)-1f, (int)1f), Main.rand.Next((int)-1f, (int)1f), 100, default(Color), 0.75f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}
