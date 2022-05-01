using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;

namespace ClickerClass.Projectiles
{
	public class SlickClickerPro : ClickerProjectile
	{
		public bool HasSpawnEffects
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public int GravityTimer
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 180;
			Projectile.alpha = 255;
			Projectile.extraUpdates = 3;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;

				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 86);
			}

			if (Projectile.timeLeft < 170)
			{
				Projectile.friendly = true;
			}

			Projectile.velocity.Y /= 1.0065f;

			GravityTimer++;

			if (GravityTimer >= 0)
			{
				Projectile.velocity.Y += 1.05f;
				GravityTimer = -15;
			}

			for (int num363 = 0; num363 < 3; num363++)
			{
				float num364 = Projectile.velocity.X / 3f * (float)num363;
				float num365 = Projectile.velocity.Y / 3f * (float)num363;
				int num366 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 33, 0f, 0f, 100, default(Color), 1.25f);
				Main.dust[num366].position.X = Projectile.Center.X - num364;
				Main.dust[num366].position.Y = Projectile.Center.Y - num365;
				Main.dust[num366].velocity *= 0f;
				Main.dust[num366].noGravity = true;
			}

			for (int k = 0; k < 2; k++)
			{
				int dust = Dust.NewDust(Projectile.position, 10, 10, 33, Main.rand.Next((int)-1f, (int)1f), Main.rand.Next((int)-1f, (int)1f), 100, default(Color), 0.75f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}
