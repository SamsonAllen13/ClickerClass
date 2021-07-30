using ClickerClass.Core;
using ClickerClass.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.Audio;

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
			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 1;
			Projectile.tileCollide = false;
		}

		public override void AI()
		{
			WobbleTimer++;
			if (WobbleTimer > 2)
			{
				Projectile.velocity.Y += Rng.NextFloat(-1f, 1f);
				Projectile.velocity.X += Rng.NextFloat(-1f, 1f);
				WobbleTimer = 0;
			}

			int index = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<MiceDust>(), 0f, 0f, 0, default(Color), 1f);
			Dust dust = Main.dust[index];
			dust.position.X = Projectile.Center.X;
			dust.position.Y = Projectile.Center.Y;
			dust.velocity *= 0f;
			dust.noGravity = true;

			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;

				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 105);
				for (int k = 0; k < 10; k++)
				{
					dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, ModContent.DustType<MiceDust>(), Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 0, default, 1.25f);
					dust.noGravity = true;
				}
			}

			if (Projectile.timeLeft < 540)
			{
				Projectile.friendly = true;
				Projectile.extraUpdates = 4;

				MousePlayer mousePlayer = Main.player[Projectile.owner].GetModPlayer<MousePlayer>();
				if (mousePlayer.TryGetMousePosition(out Vector2 mouseWorld))
				{
					float x = Projectile.Center.X;
					float y = Projectile.Center.Y;
					float dist = 1000f;
					bool found = false;

					float mouseX = mouseWorld.X;
					float mouseY = mouseWorld.Y;
					float abs = Math.Abs(Projectile.Center.X - mouseX) + Math.Abs(Projectile.Center.Y - mouseY);
					if (abs < dist)
					{
						x = mouseX;
						y = mouseY;
						found = true;
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
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, ModContent.DustType<MiceDust>(), Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1f);
				dust.noGravity = true;
			}
		}
	}
}
