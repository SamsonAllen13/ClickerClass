using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Utilities;

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
					rng = new UnifiedRandom(RandomSeed / (1 + projectile.identity));
				}
				return rng;
			}
		}

		public int RandomSeed
		{
			get => (int)projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		public bool HasSpawnEffects
		{
			get => projectile.ai[1] == 1f;
			set => projectile.ai[1] = value ? 1f : 0f;
		}

		public int WobbleTimer
		{
			get => (int)projectile.localAI[0];
			set => projectile.localAI[0] = value;
		}

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.penetrate = 1;
			projectile.alpha = 255;
			projectile.timeLeft = 180;
			projectile.extraUpdates = 2;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Ichor, 300, false);

			for (int k = 0; k < 5; k++)
			{
				Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 55, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 255, default, 1.25f);
				dust.noGravity = true;
			}
		}

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;
				Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 24);
			}

			WobbleTimer++;
			if (WobbleTimer > 2)
			{
				projectile.velocity.Y += Rng.NextFloat(-1f, 1f);
				projectile.velocity.X += Rng.NextFloat(-1f, 1f);
				WobbleTimer = 0;
			}

			int index = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 55, 0f, 0f, 255, default(Color), 1.25f);
			Dust dust = Main.dust[index];
			dust.position.X = projectile.Center.X;
			dust.position.Y = projectile.Center.Y;
			dust.velocity *= 0f;
			dust.noGravity = true;

			if (projectile.timeLeft < 150)
			{
				projectile.friendly = true;

				float x = projectile.Center.X;
				float y = projectile.Center.Y;
				float dist = 500f;
				bool found = false;

				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy() && projectile.DistanceSQ(npc.Center) < dist * dist && Collision.CanHit(projectile.Center, 1, 1, npc.Center, 1, 1))
					{
						float foundX = npc.Center.X;
						float foundY = npc.Center.Y;
						float abs = Math.Abs(projectile.Center.X - foundX) + Math.Abs(projectile.Center.Y - foundY);
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
					Vector2 center = projectile.Center;
					float toX = x - center.X;
					float toY = y - center.Y;
					float len = (float)Math.Sqrt((double)(toX * toX + toY * toY));
					len = mag / len;
					toX *= len;
					toY *= len;

					projectile.velocity.X = (projectile.velocity.X * 20f + toX) / 21f;
					projectile.velocity.Y = (projectile.velocity.Y * 20f + toY) / 21f;
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}
	}
}
