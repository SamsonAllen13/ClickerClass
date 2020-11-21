using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class SpaceClickerPro : ClickerProjectile
	{
		public int timer = 0;
		public float rotation = 0f;
		public bool colorChoice = false;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[projectile.type] = 7;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.timeLeft = 300;
			projectile.extraUpdates = 2;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			if (projectile.timeLeft > 4)
			{
				return new Color(255, 255, 255, 0) * 1f;
			}
			else
			{
				return new Color(255, 255, 255, 0) * 0f;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (projectile.timeLeft > 4)
			{
				Rectangle frame = new Rectangle(0, 0, 22, 26);
				frame.Y += 26 * projectile.frame;

				Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
				for (int k = 0; k < projectile.oldPos.Length; k++)
				{
					Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
					Color color = projectile.GetAlpha(new Color(255, 255, 255, 0) * 0.25f) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
					spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, frame, color * 0.25f, rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
				}
			}
			return true;
		}

		public override void AI()
		{
			if (!colorChoice)
			{
				projectile.frame = Main.rand.Next(7);
				colorChoice = true;
			}
			projectile.rotation += projectile.velocity.X > 0f ? 0.35f : -0.35f;

			timer++;
			if (timer % 5 == 0)
			{
				rotation = projectile.rotation;
			}

			int dustType = 90;
			if (projectile.frame == 1) { dustType = 92; }
			if (projectile.frame == 2) { dustType = 87; }
			if (projectile.frame == 3) { dustType = 89; }
			if (projectile.frame == 4) { dustType = 92; }
			if (projectile.frame == 5) { dustType = 88; }
			if (projectile.frame == 6) { dustType = 86; }

			Vector2 vec = new Vector2(projectile.ai[0], projectile.ai[1]);
			if (Vector2.Distance(projectile.Center, vec) <= 10)
			{
				if (projectile.timeLeft > 4)
				{
					Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 110);
					projectile.timeLeft = 4;

					float num102 = 30f;
					int num103 = 0;
					while ((float)num103 < num102)
					{
						Vector2 vector12 = Vector2.UnitX * 0f;
						vector12 += -Vector2.UnitY.RotatedBy((double)((float)num103 * (6.28318548f / num102)), default(Vector2)) * new Vector2(8f, 8f);
						vector12 = vector12.RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
						int num104 = Dust.NewDust(projectile.Center, 0, 0, dustType, 0f, 0f, 0, default(Color), 1.25f);
						if (projectile.frame == 1) { Main.dust[num104].shader = GameShaders.Armor.GetSecondaryShader(70, Main.LocalPlayer); }
						Main.dust[num104].noGravity = true;
						Main.dust[num104].position = projectile.Center + vector12;
						Main.dust[num104].velocity = projectile.velocity * 0f + vector12.SafeNormalize(Vector2.UnitY) * 5f;
						int num = num103;
						num103 = num + 1;
					}
				}
			}

			if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3)
			{
				projectile.velocity.X = 0f;
				projectile.velocity.Y = 0f;
				projectile.tileCollide = false;
				projectile.friendly = true;
				projectile.alpha = 255;
				projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
				projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
				projectile.width = 150;
				projectile.height = 150;
				projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
				projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
			}
		}
	}
}