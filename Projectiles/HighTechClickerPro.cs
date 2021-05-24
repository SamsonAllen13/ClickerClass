using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Utilities;

namespace ClickerClass.Projectiles
{
	public class HighTechClickerPro : ClickerProjectile
	{
		private readonly HashSet<int> hitTargets = new HashSet<int>();
		private readonly HashSet<int> foundTargets = new HashSet<int>();

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
			projectile.penetrate = -1;
			projectile.alpha = 255;
			projectile.friendly = true;
			projectile.timeLeft = 600;
			projectile.extraUpdates = 100;

			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
		}

		public override bool? CanHitNPC(NPC target)
		{
			if (hitTargets.Contains(target.whoAmI))
			{
				return false;
			}
			return base.CanHitNPC(target);
		}

		public override void AI()
		{
			bool killed = projectile.HandleChaining(hitTargets, foundTargets, 8);
			if (killed)
			{
				return;
			}

			int index = Dust.NewDust(projectile.position, projectile.width, projectile.height, 229, 0f, 0f, 0, default(Color), 1.15f);
			Dust dust = Main.dust[index];
			dust.position.X = projectile.Center.X;
			dust.position.Y = projectile.Center.Y;
			dust.velocity *= 0f;
			dust.noGravity = true;

			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;

				Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 94);

				for (int k = 0; k < 20; k++)
				{
					dust = Dust.NewDustDirect(projectile.Center - new Vector2(4), 8, 8, 229, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 0, default, 1.25f);
					dust.noGravity = true;
				}
			}

			if (projectile.timeLeft < 580)
			{
				if (projectile.timeLeft >= 600)
				{
					for (int k = 0; k < 6; k++)
					{
						index = Dust.NewDust(projectile.position, projectile.width, projectile.height, 229, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 125, default(Color), 1.15f);
						Main.dust[index].noGravity = true;
					}
				}

				WobbleTimer++;
				if (WobbleTimer > 2)
				{
					projectile.velocity.Y += Rng.NextFloat(-0.75f, 0.75f);
					projectile.velocity.X += Rng.NextFloat(-0.75f, 0.75f);
					WobbleTimer = 0;
				}

				float x = projectile.Center.X;
				float y = projectile.Center.Y;
				float dist = 750;
				bool found = false;

				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy() && !hitTargets.Contains(npc.whoAmI) && projectile.DistanceSQ(npc.Center) < dist * dist && Collision.CanHit(projectile.Center, 1, 1, npc.Center, 1, 1))
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
					float mag = 2.5f;
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
				else
				{
					projectile.velocity = Vector2.Zero;
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 10; k++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 229, Main.rand.Next((int)-5f, (int)5f), Main.rand.Next((int)-5f, (int)5f), 75, default(Color), 0.75f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}
