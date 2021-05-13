using ClickerClass.Core;
using ClickerClass.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ClickerClass.Projectiles
{
	public class MiceClickerPro : ClickerProjectile
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
			projectile.timeLeft = 600;
			projectile.extraUpdates = 1;
			projectile.tileCollide = false;
		}

		public override void AI()
		{
			WobbleTimer++;
			if (WobbleTimer > 2)
			{
				projectile.velocity.Y += Rng.NextFloat(-1f, 1f);
				projectile.velocity.X += Rng.NextFloat(-1f, 1f);
				WobbleTimer = 0;
			}

			int index = Dust.NewDust(projectile.Center, projectile.width, projectile.height, ModContent.DustType<MiceDust>(), 0f, 0f, 0, default(Color), 1f);
			Dust dust = Main.dust[index];
			dust.position.X = projectile.Center.X;
			dust.position.Y = projectile.Center.Y;
			dust.velocity *= 0f;
			dust.noGravity = true;

			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;

				Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 105);
				for (int k = 0; k < 10; k++)
				{
					dust = Dust.NewDustDirect(projectile.Center - new Vector2(4), 8, 8, ModContent.DustType<MiceDust>(), Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 0, default, 1.25f);
					dust.noGravity = true;
				}
			}

			if (projectile.timeLeft < 540)
			{
				projectile.friendly = true;
				projectile.extraUpdates = 4;

				MousePlayer mousePlayer = Main.player[projectile.owner].GetModPlayer<MousePlayer>();
				mousePlayer.SetMousePosition();
				if (mousePlayer.TryGetMousePosition(out Vector2 mouseWorld))
				{
					float x = projectile.Center.X;
					float y = projectile.Center.Y;
					float dist = 1000f;
					bool found = false;

					float mouseX = mouseWorld.X;
					float mouseY = mouseWorld.Y;
					float abs = Math.Abs(projectile.Center.X - mouseX) + Math.Abs(projectile.Center.Y - mouseY);
					if (abs < dist)
					{
						x = mouseX;
						y = mouseY;
						found = true;
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
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, ModContent.DustType<MiceDust>(), Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1f);
				dust.noGravity = true;
			}
		}
	}
}
