using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class WebClickerPro : ClickerProjectile
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 60;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color * 0.25f, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
		
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCHit, (int)projectile.position.X, (int)projectile.position.Y, 11);
			float leftFacing = projectile.velocity.X > 0f ? 1f : 0f;
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<WebClickerPro2>(), (int)(projectile.damage * 0.50), 1f, projectile.owner, leftFacing, 0f);

			for (int k = 0; k < 5; k++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 261, Main.rand.Next((int)-4f, (int)4f), Main.rand.Next((int)-4f, (int)4f), 0, default(Color), 0.8f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}