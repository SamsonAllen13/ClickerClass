using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class SandstormClickerPro : ClickerProjectile
	{
		public float alpha = 0.85f;
		public bool fadeOut = false;
		public bool homing = false;
		List<int> targets = new List<int>();

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		
		public override void SetDefaults()
		{
			projectile.width = 60;
			projectile.height = 60;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.alpha = 255;
			projectile.magic = true;
			projectile.friendly = true;
			projectile.timeLeft = 600;
			projectile.extraUpdates = 2;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			SpriteEffects effects = projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				float scaleDown = 0.067f * (15 - k);
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				spriteBatch.Draw(mod.GetTexture("Projectiles/SandstormClickerPro_Effect"), drawPos, null, new Color(255, 255, 255, 0) * (alpha * 0.25f), projectile.rotation, drawOrigin, scaleDown, effects, 0f);
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 100) * alpha;
		}
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = projectile.width / 2;
			height = projectile.height / 2;
			return true;
		}

		public override bool? CanHitNPC(NPC target)
		{
			if (targets.Contains(target.whoAmI))
			{
				return false;
			}
			return base.CanHitNPC(target);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			targets.Add(target.whoAmI);
			if (targets.Count >= 10)
			{
				projectile.Kill();
			}
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			if (projectile.ai[0] != 0f)
			{
				targets.Add((int)(projectile.ai[0]));
				projectile.ai[0] = 0f;
			}
			
			projectile.rotation += projectile.velocity.X > 0f ? 0.08f : -0.08f;
			projectile.spriteDirection = projectile.velocity.X > 0f ? 1 : -1;

			int num3;
			float num477 = player.Center.X;
			float num478 = player.Center.Y;
			float num479 = 800f;
			bool flag17 = false;

			for (int num480 = 0; num480 < Main.maxNPCs; num480 = num3 + 1)
			{
				if (Main.npc[num480].active && !targets.Contains(Main.npc[num480].whoAmI) && Main.npc[num480].CanBeChasedBy(projectile, false) && projectile.Distance(Main.npc[num480].Center) < num479 && Collision.CanHit(projectile.Center, 1, 1, Main.npc[num480].Center, 1, 1))
				{
					float num481 = Main.npc[num480].position.X + (float)(Main.npc[num480].width / 2);
					float num482 = Main.npc[num480].position.Y + (float)(Main.npc[num480].height / 2);
					float num483 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num481) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num482);
					if (num483 < num479)
					{
						num479 = num483;
						num477 = num481;
						num478 = num482;
						flag17 = true;
					}
				}
				num3 = num480;
			}

			if (flag17)
			{
				float num488 = 4f;

				Vector2 vector38 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
				float num489 = num477 - vector38.X;
				float num490 = num478 - vector38.Y;
				float num491 = (float)Math.Sqrt((double)(num489 * num489 + num490 * num490));
				num491 = num488 / num491;
				num489 *= num491;
				num490 *= num491;

				projectile.velocity.X = (projectile.velocity.X * 20f + num489) / 21f;
				projectile.velocity.Y = (projectile.velocity.Y * 20f + num490) / 21f;
				homing = true;
			}
			else
			{
				homing = false;
			}
			
			if (projectile.timeLeft < 30)
			{
				fadeOut = true;
			}
			
			if (fadeOut)
			{
				alpha -= 0.05f;
				if (alpha < 0f)
				{
					projectile.Kill();
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (!homing && !fadeOut)
			{
				fadeOut = true;
				projectile.friendly = false;
				projectile.tileCollide = false;
				projectile.timeLeft = 30;
				projectile.velocity = oldVelocity;
			}
			return false;
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 10; k++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 216, Main.rand.Next((int)-5f, (int)5f), Main.rand.Next((int)-5f, (int)5f), 200, default(Color), 1.25f);
				Main.dust[dust].noGravity = true;
			}
			for (int k = 0; k < 10; k++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 216, Main.rand.Next((int)-3f, (int)3f), Main.rand.Next((int)-3f, (int)3f), 50, default(Color), 1f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}