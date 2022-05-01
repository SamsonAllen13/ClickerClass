using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;

namespace ClickerClass.Projectiles
{
	public class HemoClickerPro : ClickerProjectile
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
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
			Projectile.alpha = 255;
			Projectile.extraUpdates = 1;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;

				SoundEngine.PlaySound(SoundID.NPCHit, (int)Projectile.Center.X, (int)Projectile.Center.Y, 13);
			}

			if (Projectile.timeLeft < 280)
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
				float velX = Projectile.velocity.X / 3f * num363;
				float velY = Projectile.velocity.Y / 3f * num363;
				int index = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 183, 0f, 0f, 75, default(Color), 1.35f);
				Dust dust = Main.dust[index];
				dust.position.X = Projectile.Center.X - velX;
				dust.position.Y = Projectile.Center.Y - velY;
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
