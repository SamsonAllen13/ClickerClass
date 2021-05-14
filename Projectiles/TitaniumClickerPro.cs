using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class TitaniumClickerPro : ClickerProjectile
	{
		public int radiusIncrease = 0;
		public int timer = 0;
		public float rotation = 0f;
		public Vector2 center = Vector2.Zero;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 30;
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
			projectile.extraUpdates = 1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor * 0.25f) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color * (0.0025f * projectile.timeLeft), rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		public static Vector2 RotateVector(Vector2 origin, Vector2 vecToRot, float rot)
		{
			float newPosX = (float)(Math.Cos(rot) * (vecToRot.X - origin.X) - Math.Sin(rot) * (vecToRot.Y - origin.Y) + origin.X);
			float newPosY = (float)(Math.Sin(rot) * (vecToRot.X - origin.X) + Math.Cos(rot) * (vecToRot.Y - origin.Y) + origin.Y);
			return new Vector2(newPosX, newPosY);
		}

		public float rot = 0f;

		public override void AI()
		{
			if (center == Vector2.Zero)
			{
				center = projectile.Center;
			}
			projectile.rotation += projectile.velocity.X > 0f ? 0.2f : -0.2f;

			timer++;
			if (timer % 5 == 0)
			{
				rotation = projectile.rotation;
			}

			radiusIncrease += 1;
			rot += 0.05f;
			projectile.Center = center + RotateVector(default(Vector2), new Vector2(0, 20 + radiusIncrease), rot + (projectile.ai[0] * (6.28f / 5)));
		}
	}
}