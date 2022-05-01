using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Audio;
using Terraria.GameContent;

namespace ClickerClass.Projectiles
{
	public class SpaceClickerPro : ClickerProjectile
	{
		public Vector2 Location => new Vector2(Projectile.ai[0], Projectile.ai[1]);
		public bool colorChoice = false;
		
		public int Timer
		{
			get => (int)Projectile.localAI[0];
			set => Projectile.localAI[0] = value;
		}

		public float Rotation
		{
			get => Projectile.localAI[1];
			set => Projectile.localAI[1] = value;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 7;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 300;
			Projectile.extraUpdates = 2;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			if (Projectile.timeLeft > 4)
			{
				return new Color(255, 255, 255, 0) * 1f;
			}
			return Color.Transparent;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			if (Projectile.timeLeft > 4)
			{
				Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
				Rectangle frame = texture2D.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);

				Vector2 drawOrigin = new Vector2(texture2D.Width * 0.5f, Projectile.height * 0.5f);
				for (int k = 0; k < Projectile.oldPos.Length; k++)
				{
					Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
					Color color = Projectile.GetAlpha(new Color(255, 255, 255, 0) * 0.25f) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
					Main.EntitySpriteDraw(texture2D, drawPos, frame, color * 0.25f, Rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
				}
			}
			return true;
		}

		public override void AI()
		{
			if (!colorChoice)
			{
				Projectile.frame = Main.rand.Next(7);
				colorChoice = true;
			}
			Projectile.rotation += Projectile.velocity.X > 0f ? 0.35f : -0.35f;

			Timer++;
			if (Timer % 5 == 0)
			{
				Rotation = Projectile.rotation;
			}

			int dustType = 90;
			if (Projectile.frame == 1) { dustType = 92; }
			if (Projectile.frame == 2) { dustType = 87; }
			if (Projectile.frame == 3) { dustType = 89; }
			if (Projectile.frame == 4) { dustType = 92; }
			if (Projectile.frame == 5) { dustType = 88; }
			if (Projectile.frame == 6) { dustType = 86; }

			Vector2 vec = Location;
			if (Projectile.DistanceSQ(vec) <= 10 * 10)
			{
				if (Projectile.timeLeft > 4)
				{
					SoundEngine.PlaySound(2, (int)Projectile.Center.X, (int)Projectile.Center.Y, 110);
					Projectile.timeLeft = 4;

					float max = 30f;
					int i = 0;
					while (i < max)
					{
						Vector2 vector12 = Vector2.Zero;
						vector12 += -Vector2.UnitY.RotatedBy(i * (MathHelper.TwoPi / max), default(Vector2)) * new Vector2(8f, 8f);
						vector12 = vector12.RotatedBy(Projectile.velocity.ToRotation(), default(Vector2));
						int index = Dust.NewDust(Projectile.Center, 0, 0, dustType, 0f, 0f, 0, default(Color), 1.25f);
						Dust dust = Main.dust[index];
						if (Projectile.frame == 1) { dust.shader = GameShaders.Armor.GetSecondaryShader(70, Main.LocalPlayer); }
						dust.noGravity = true;
						dust.position = Projectile.Center + vector12;
						dust.velocity = Projectile.velocity * 0f + vector12.SafeNormalize(Vector2.UnitY) * 5f;
						i++;
					}
				}
			}

			if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
			{
				Projectile.velocity.X = 0f;
				Projectile.velocity.Y = 0f;
				Projectile.tileCollide = false;
				Projectile.friendly = true;
				Projectile.alpha = 255;
				Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
				Projectile.width = 150;
				Projectile.height = 150;
				Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			}
		}
	}
}
