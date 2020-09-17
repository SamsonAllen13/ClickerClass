using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;

namespace ClickerClass.Projectiles
{
	public class CaptainsClickerPro : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.timeLeft = 300;
			projectile.extraUpdates = 1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[projectile.owner];
			damage = (int)(damage + (target.defense / 2));
			hitDirection = target.Center.X < player.Center.X ? -1 : 1;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 300, false);
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (projectile.timeLeft > 4)
			{
				Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
				for (int k = 0; k < projectile.oldPos.Length; k++)
				{
					Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
					Color color = projectile.GetAlpha(lightColor * 0.25f) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
					spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color * 0.25f, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
				}
			}
			return true;
		}

		public override void AI()
		{
			projectile.rotation += projectile.velocity.X > 0f ? 0.35f : -0.35f;
			
			for (int l = 0; l < 2; l++)
			{
				int num235 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y) - projectile.velocity, projectile.width, projectile.height, 31, 0f, 0f, 125, default(Color), 1f);
				Dust dust4 = Main.dust[num235];
				dust4 = Main.dust[num235];
				dust4.velocity *= 0f;
				Main.dust[num235].noGravity = true;
			}
			
			Vector2 vec = new Vector2(projectile.ai[0], projectile.ai[1]);
			if (Vector2.Distance(projectile.Center, vec) <= 10)
			{
				if (projectile.timeLeft > 4)
				{
					Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 89);
					projectile.timeLeft = 4;
					
					for (int k = 0; k < 10; k++)
					{
						Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 6, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 0, default, 1.35f);
						dust.noGravity = true;
					}
					for (int k = 0; k < 20; k++)
					{
						Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 174, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 0, default, 1.35f);
						dust.noGravity = true;
					}
					for (int k = 0; k < 15; k++)
					{
						Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 31, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 125, default, 1f);
						dust.noGravity = true;
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
				projectile.width = 200;
				projectile.height = 200;
				projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
				projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
			}
		}
	}
}