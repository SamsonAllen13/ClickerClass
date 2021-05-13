using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class OrichaclumClickerPro : ClickerProjectile
	{
		public int Frame
		{
			get => (int)projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		public int Timer
		{
			get => (int)projectile.ai[1];
			set => projectile.ai[1] = value;
		}

		public bool Spawned
		{
			get => projectile.localAI[0] == 1f;
			set => projectile.localAI[0] = value ? 1f : 0f;
		}

		public float Rotation
		{
			get => projectile.localAI[1];
			set => projectile.localAI[1] = value;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[projectile.type] = 3;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.timeLeft = 240;
			projectile.extraUpdates = 3;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 0) * (0.01f * projectile.timeLeft);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Rectangle frame = new Rectangle(0, 0, 20, 20);
			frame.Y += 20 * projectile.frame;

			Texture2D texture2D = Main.projectileTexture[projectile.type];
			Vector2 drawOrigin = new Vector2(texture2D.Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(new Color(255, 255, 255, 0) * 0.25f) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(texture2D, drawPos, frame, color * (0.0025f * projectile.timeLeft), Rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		public override void AI()
		{
			projectile.frame = Frame;
			projectile.rotation += projectile.velocity.X > 0f ? 0.2f : -0.2f;

			Timer++;
			if (Timer % 5 == 0)
			{
				Rotation = projectile.rotation;
			}

			if (!Spawned)
			{
				Spawned = true;

				float max = 30f;
				int i = 0;
				while (i < max)
				{
					Vector2 vector2 = Vector2.Zero;
					vector2 += -Vector2.UnitY.RotatedBy(i * (MathHelper.TwoPi / max)) * new Vector2(2f, 2f);
					vector2 = vector2.RotatedBy(projectile.velocity.ToRotation(), default(Vector2));
					int index = Dust.NewDust(projectile.Center, 0, 0, 86, 0f, 0f, 0, default(Color), 1.25f);
					Dust dust = Main.dust[index];
					dust.noGravity = true;
					dust.position = projectile.Center + vector2;
					dust.velocity = vector2.SafeNormalize(Vector2.UnitY) * 2f;
					i++;
				}
			}
		}
	}
}
