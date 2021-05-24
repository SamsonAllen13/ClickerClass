using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class MyceliumClickerPro : ClickerProjectile
	{
		public bool HasSpawnEffects
		{
			get => projectile.ai[0] == 1f;
			set => projectile.ai[0] = value ? 1f : 0f;
		}

		public int Timer
		{
			get => (int)projectile.ai[1];
			set => projectile.ai[1] = value;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[projectile.type] = 4;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		
		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.penetrate = 1;
			projectile.timeLeft = 180;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Rectangle frame = new Rectangle(0, 0, 14, 20);
			frame.Y += 20 * projectile.frame;

			Texture2D texture2D = Main.projectileTexture[projectile.type];
			Vector2 drawOrigin = new Vector2(texture2D.Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(texture2D, drawPos, frame, color * 0.25f, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;

				Main.PlaySound(3, (int)projectile.Center.X, (int)projectile.Center.Y, 13);
				for (int k = 0; k < 20; k++)
				{
					Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 41, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 200, default, 1.25f);
					dust.noGravity = true;
				}
			}

			if (projectile.timeLeft < 170)
			{
				projectile.friendly = true;
			}

			Timer++;
			if (Timer >= 0)
			{
				projectile.velocity.Y += 1.015f;
				Timer = -15;
			}
		}
		
		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 8; k++)
			{
				int dust = Dust.NewDust(projectile.position, (int)(projectile.width * 0.5f), (int)(projectile.height * 0.5f), 41, Main.rand.Next((int)-8f, (int)8f), Main.rand.Next((int)-8f, (int)8f), 200, default, 1.25f);
				Main.dust[dust].noGravity = true;
			}
		}
		
		public override void PostAI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter > 8)
			{
				projectile.frame++;
				projectile.frameCounter = 0;
			}
			if (projectile.frame >= 4)
			{
				projectile.frame = 0;
			}
		}
	}
}
