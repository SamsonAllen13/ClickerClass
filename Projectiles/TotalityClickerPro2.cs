using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Utilities;

namespace ClickerClass.Projectiles
{
	public class TotalityClickerPro2 : ClickerProjectile
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

		public int WobbleTimer
		{
			get => (int)projectile.ai[1];
			set => projectile.ai[1] = value;
		}

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.penetrate = 1;
			projectile.alpha = 255;
			projectile.timeLeft = 180;
			projectile.extraUpdates = 3;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 264, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), 0, default, 1.25f);
				dust.noGravity = true;
			}
		}

		public override void AI()
		{
			if (projectile.timeLeft < 172)
			{
				WobbleTimer++;
				if (WobbleTimer > 2)
				{
					projectile.velocity.Y += Rng.NextFloat(-1f, 1f);
					projectile.velocity.X += Rng.NextFloat(-1f, 1f);
					WobbleTimer = 0;
				}

				int index = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 264, 0f, 0f, 0, new Color(255, 255, 255, 0), 1.5f);
				Dust dust = Main.dust[index];
				dust.position.X = projectile.Center.X;
				dust.position.Y = projectile.Center.Y;
				dust.velocity *= 0f;
				dust.noGravity = true;
			}

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
					float mag = 3f;
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
