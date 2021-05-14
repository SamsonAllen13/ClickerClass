using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class MyceliumClickerPro : ClickerProjectile
	{
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
			
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, frame, color * 0.25f, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		public override void AI()
		{
			if (projectile.timeLeft < 170)
			{
				projectile.friendly = true;
			}
			
			projectile.ai[1]++;
			if (projectile.ai[1] >= 0)
			{
				projectile.velocity.Y += 1.015f;
				projectile.ai[1] = -15;
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
				return;
			}
		}
	}
}