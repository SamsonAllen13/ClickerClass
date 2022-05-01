using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class OrichaclumClickerPro : ClickerProjectile
	{
		public int Frame
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public int Timer
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public bool Spawned
		{
			get => Projectile.localAI[0] == 1f;
			set => Projectile.localAI[0] = value ? 1f : 0f;
		}

		public float Rotation
		{
			get => Projectile.localAI[1];
			set => Projectile.localAI[1] = value;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 3;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 240;
			Projectile.extraUpdates = 3;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 0) * (0.01f * Projectile.timeLeft);
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
			Rectangle frame = texture2D.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);

			Vector2 drawOrigin = new Vector2(texture2D.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(new Color(255, 255, 255, 0) * 0.25f) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture2D, drawPos, frame, color * (0.0025f * Projectile.timeLeft), Rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}

		public override void AI()
		{
			Projectile.frame = Frame;
			Projectile.rotation += Projectile.velocity.X > 0f ? 0.2f : -0.2f;

			Timer++;
			if (Timer % 5 == 0)
			{
				Rotation = Projectile.rotation;
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
					vector2 = vector2.RotatedBy(Projectile.velocity.ToRotation(), default(Vector2));
					int index = Dust.NewDust(Projectile.Center, 0, 0, 86, 0f, 0f, 0, default(Color), 1.25f);
					Dust dust = Main.dust[index];
					dust.noGravity = true;
					dust.position = Projectile.Center + vector2;
					dust.velocity = vector2.SafeNormalize(Vector2.UnitY) * 2f;
					i++;
				}
			}
		}
	}
}
