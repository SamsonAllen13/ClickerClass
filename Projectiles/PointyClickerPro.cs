using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class PointyClickerPro : ClickerProjectile
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.friendly = true;
			projectile.timeLeft = 120;
			projectile.extraUpdates = 2;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 60;
		}

		public override void AI()
		{
			if (projectile.localAI[0] < 48)
			{
				projectile.localAI[0]++;
			}

			if (projectile.ai[1] > 0f)
			{
				projectile.SineWaveMovement(projectile.ai[0], 10f, MathHelper.TwoPi / 40, projectile.ai[0] == 0);
			}
			else
			{
				projectile.SineWaveMovement(projectile.ai[0], -10f, MathHelper.TwoPi / 40, projectile.ai[0] == 0);
			}
			projectile.ai[0]++;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int i = projectile.oldPos.Length - 1; i >= 0; i--)
			{
				Vector2 drawPos = projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor * 0.25f) * ((projectile.oldPos.Length - i) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.oldRot[i], drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return false;
		}
	}
}