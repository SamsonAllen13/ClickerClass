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

		public int WobbleTimer
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
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
			Projectile.extraUpdates = 3;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 264, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), 0, default, 1.25f);
				dust.noGravity = true;
			}
		}

		public override void AI()
		{
			if (Projectile.timeLeft < 172)
			{
				WobbleTimer++;
				if (WobbleTimer > 2)
				{
					Projectile.velocity.Y += Rng.NextFloat(-1f, 1f);
					Projectile.velocity.X += Rng.NextFloat(-1f, 1f);
					WobbleTimer = 0;
				}

				int index = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 264, 0f, 0f, 0, new Color(255, 255, 255, 0), 1.5f);
				Dust dust = Main.dust[index];
				dust.position.X = Projectile.Center.X;
				dust.position.Y = Projectile.Center.Y;
				dust.velocity *= 0f;
				dust.noGravity = true;
			}

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
					float mag = 3f;
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
