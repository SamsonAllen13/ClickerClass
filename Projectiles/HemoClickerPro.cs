using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class HemoClickerPro : ClickerProjectile
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
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
			projectile.alpha = 255;
			projectile.extraUpdates = 1;
			projectile.ignoreWater = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
		}

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;

				Main.PlaySound(SoundID.NPCHit, (int)projectile.Center.X, (int)projectile.Center.Y, 13);
			}

			if (projectile.timeLeft < 280)
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
				float velX = projectile.velocity.X / 3f * num363;
				float velY = projectile.velocity.Y / 3f * num363;
				int index = Dust.NewDust(projectile.position, projectile.width, projectile.height, 183, 0f, 0f, 75, default(Color), 1.35f);
				Dust dust = Main.dust[index];
				dust.position.X = projectile.Center.X - velX;
				dust.position.Y = projectile.Center.Y - velY;
				dust.velocity *= 0f;
				dust.noGravity = true;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}
	}
}
