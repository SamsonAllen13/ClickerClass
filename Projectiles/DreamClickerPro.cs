using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class DreamClickerPro : ClickerProjectile
	{
		public int proTimer = 45;
		public Vector2 basePos;
		public int baseDir;
		public int length = 180;
		public double t;
		public double startTheta;
		public double endTheta;
		public double currTheta;
		public int i;

		public bool HasSpawnEffects
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public bool fadeOut = false;

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 42;
			Projectile.height = 42;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = proTimer;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 50) * (0.75f * Projectile.Opacity);
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
			Vector2 drawOrigin = new Vector2(texture2D.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);

				Main.EntitySpriteDraw(texture2D, drawPos, null, color * 0.5f, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			if (Projectile.timeLeft == proTimer)
			{
				basePos = Projectile.position;
				baseDir = player.direction;
				i = (int)Projectile.ai[1];

				// Set polar curve theta boundaries
				startTheta = MathHelper.PiOver2 * i;
				endTheta = (((3 * i) + 4) * MathHelper.Pi) / 6.0;
				if (baseDir == -1)
				{
					startTheta += MathHelper.Pi / 3.0;
					endTheta += MathHelper.Pi / 3.0;
				}
			}

			if (HasSpawnEffects)
			{
				SoundEngine.PlaySound(SoundID.Item43, Projectile.Center);
				SoundEngine.PlaySound(SoundID.Item4.WithVolumeScale(0.8f), Projectile.Center);
				SoundEngine.PlaySound(SoundID.Item71.WithVolumeScale(0.6f), Projectile.Center);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 8, 8, 57, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 255, default, 1.25f);
					dust.noGravity = true;
				}
				HasSpawnEffects = false;
			}

			HandlePosition();
			HandleExtra();
			SpawnDust();
		}

		private void HandlePosition()
		{
			// Curve function:
			//  if dir == 1:  r = sin( 1.5theta + (ipi/4) ),
			//    ipi/2 <= theta <= (3i + 4)pi/6
			//  if dir == -1: r = sin( 1.5theta + (ipi/4) + pi/2 ),
			//    (3i + 4)pi/6 + pi/3 <= theta <= ipi/2 + pi/3

			// Calculate frame theta based on projectile time
			t = 1.0 - ((double)Projectile.timeLeft / proTimer);

			double parametricA;
			double parametricB;
			if (baseDir == -1)
			{
				parametricA = (t) * startTheta;
				parametricB = (1 - t) * endTheta;
			}
			else
			{
				parametricA = (1 - t) * startTheta;
				parametricB = (t) * endTheta;
			}
			currTheta = parametricA + parametricB;

			// Calculate polar function
			double p = (MathHelper.Pi * i) / 4.0;
			double rho;
			if (baseDir == -1)
			{
				rho = Math.Sin((1.5 * currTheta) + p + MathHelper.PiOver2) * length;
			}
			else
			{
				rho = Math.Sin((1.5 * currTheta) + p) * length;
			}

			// Convert from polar to Cartesian
			Projectile.position.X = (float)(basePos.X + (rho * Math.Cos(currTheta)));
			Projectile.position.Y = (float)(basePos.Y + (rho * Math.Sin(currTheta)));
		}

		private void HandleExtra()
		{
			// Scale
			Projectile.scale = MathHelper.Lerp(0.75f, 1.5f, (float)(Utils.GetLerpValue(0.1f, 0.5f, t, true) * Utils.GetLerpValue(0.9f, 0.5f, t, true)));

			// Rotation
			if (baseDir == -1)
			{
				Projectile.rotation -= (2.5f * MathHelper.TwoPi) / proTimer;
			}
			else
			{
				Projectile.rotation += (2.5f * MathHelper.TwoPi) / proTimer;
			}

			// Fade out
			if (Projectile.timeLeft < 15)
			{
				fadeOut = true;
			}
			if (fadeOut)
			{
				Projectile.alpha += 15;
				if (Projectile.alpha > 255)
				{
					Projectile.alpha = 255;
				}
			}
		}

		private void SpawnDust()
		{
			if (Main.netMode == NetmodeID.Server)
			{
				return;
			}

			// Sparkle dust
			if (Main.rand.NextBool(4))
			{
				for (int i = 0; i < Main.rand.Next(2); i++)
				{
					int starDust = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 57, 0f, 0f, 150, new Color(120, 5, 90), 0.8f);
					Main.dust[starDust].velocity = Vector2.Zero;
				}
			}

			// Star gore
			if (Main.rand.NextBool(12))
			{
				var source = Projectile.GetSource_FromThis();
				for (int i = 0; i < Main.rand.Next(2); i++)
				{
					int star = Gore.NewGore(source, Projectile.Center, Vector2.Zero, Main.rand.Next(16, 18), Main.rand.NextFloat(0.5f, 0.25f));
					Main.gore[star].rotation = Main.rand.NextFloat(MathHelper.TwoPi);
					Main.gore[star].velocity = Vector2.Zero;
					Main.gore[star].alpha = Main.rand.Next(96, 161);
				}
			}
		}
	}
}
