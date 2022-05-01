using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.Audio;

namespace ClickerClass.Projectiles
{
	public class CrimsonClickerPro : ClickerProjectile
	{
		private UnifiedRandom rng;

		public UnifiedRandom Rng
		{
			get
			{
				if (rng == null)
				{
					rng = new UnifiedRandom(RandomSeed / (1 + Projectile.identity));
				}
				return rng;
			}
		}

		public int RandomSeed
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public bool HasSpawnEffects
		{
			get => Projectile.ai[1] == 1f;
			set => Projectile.ai[1] = value ? 1f : 0f;
		}

		public int WobbleTimer
		{
			get => (int)Projectile.localAI[0];
			set => Projectile.localAI[0] = value;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.aiStyle = -1;
			Projectile.penetrate = 1;
			Projectile.alpha = 255;
			Projectile.timeLeft = 180;
			Projectile.extraUpdates = 2;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Ichor, 300, false);

			for (int k = 0; k < 5; k++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 55, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 255, default, 1.25f);
				dust.noGravity = true;
			}
		}

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;
				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 24);
			}

			WobbleTimer++;
			if (WobbleTimer > 2)
			{
				Projectile.velocity.Y += Rng.NextFloat(-1f, 1f);
				Projectile.velocity.X += Rng.NextFloat(-1f, 1f);
				WobbleTimer = 0;
			}

			int index = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 55, 0f, 0f, 255, default(Color), 1.25f);
			Dust dust = Main.dust[index];
			dust.position.X = Projectile.Center.X;
			dust.position.Y = Projectile.Center.Y;
			dust.velocity *= 0f;
			dust.noGravity = true;

			if (Projectile.timeLeft < 150)
			{
				Projectile.friendly = true;

				float x = Projectile.Center.X;
				float y = Projectile.Center.Y;
				float dist = 500f;
				bool found = false;

				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy() && Projectile.DistanceSQ(npc.Center) < dist * dist && Collision.CanHit(Projectile.Center, 1, 1, npc.Center, 1, 1))
					{
						float foundX = npc.Center.X;
						float foundY = npc.Center.Y;
						float abs = Math.Abs(Projectile.Center.X - foundX) + Math.Abs(Projectile.Center.Y - foundY);
						if (abs < dist)
						{
							dist = abs;
							x = foundX;
							y = foundY;
							found = true;
						}
					}
				}

				if (found)
				{
					float mag = 7.5f;
					Vector2 center = Projectile.Center;
					float toX = x - center.X;
					float toY = y - center.Y;
					float len = (float)Math.Sqrt((double)(toX * toX + toY * toY));
					len = mag / len;
					toX *= len;
					toY *= len;

					Projectile.velocity.X = (Projectile.velocity.X * 20f + toX) / 21f;
					Projectile.velocity.Y = (Projectile.velocity.Y * 20f + toY) / 21f;
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}
	}
}
