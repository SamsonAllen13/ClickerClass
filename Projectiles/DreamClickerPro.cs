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
		public static readonly int projectileTimeLeftMax = 45;
		private float progress;
		public Vector2 origin;
		public int direction;
		public float rotationOffset = 0f;
		public const float roseDistance = 16f * 8.5f;
		public const float roseMultiplier = 2.5f; // Adjusts width of curve
		private float thetaMax;

		private bool HasSpawnEffects
		{
			get => Projectile.ai[0] == 0f;
			set => Projectile.ai[0] = value ? 0f : -1f;
		}

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;

			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Arcane);
			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Celestial);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 42;
			Projectile.height = 42;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = projectileTimeLeftMax;
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
			if (Projectile.timeLeft == projectileTimeLeftMax)
			{
				Player player = Main.player[Projectile.owner];

				origin = Projectile.Center;
				direction = player.direction;
				rotationOffset = Vector2.UnitX.RotatedBy(MathHelper.PiOver2 * (int)Projectile.ai[0]).ToRotation();
				thetaMax = MathHelper.Pi / roseMultiplier;
			}

			if (HasSpawnEffects)
			{
				SoundEngine.PlaySound(SoundID.Item43, Projectile.Center);
				SoundEngine.PlaySound(SoundID.Item4.WithVolumeScale(0.8f), Projectile.Center);
				SoundEngine.PlaySound(SoundID.Item71.WithVolumeScale(0.6f), Projectile.Center);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 8, 8, DustID.Enchanted_Gold, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 255, default, 1.25f);
					dust.noGravity = true;
				}
				HasSpawnEffects = false;
			}

			progress = (projectileTimeLeftMax - Projectile.timeLeft) / (float)projectileTimeLeftMax;

			HandlePosition();
			HandleExtra();
			SpawnDust();
		}

		private void HandlePosition()
		{
			// Rose curve with theta bound to (0, thetaMax) to only produce 1 'petal'
			float theta = progress * thetaMax;
			float r = (roseDistance * direction) * (float)Math.Sin(theta * roseMultiplier);

			// Polar to Cartesian coordinates
			float x = r * (float)Math.Cos(theta);
			float y = -r * (float)Math.Sin(theta);

			// Rotate and offset
			Vector2 offset = new Vector2(x, y).RotatedBy(rotationOffset);
			Projectile.Center = origin + offset;
		}

		private void HandleExtra()
		{
			// Scale
			float bellCurve = (float)Math.Sin(MathHelper.Pi * Utils.GetLerpValue(0.1f, 0.9f, progress, true));
			Projectile.scale = MathHelper.Lerp(0.75f, 1.5f, bellCurve);

			// Rotation
			Projectile.rotation += direction * ((2.5f * MathHelper.TwoPi) / projectileTimeLeftMax);

			// Fade out
			if (Projectile.timeLeft < 15)
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
					int starDust = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Enchanted_Gold, 0f, 0f, 150, new Color(120, 5, 90), 0.8f);
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
