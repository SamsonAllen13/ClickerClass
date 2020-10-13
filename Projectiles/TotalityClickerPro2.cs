using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class TotalityClickerPro2 : ClickerProjectile
	{
		public override void SetDefaults()
		{
			isClickerProj = true;
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.penetrate = 1;
			projectile.alpha = 255;
			projectile.timeLeft = 180;
			projectile.extraUpdates = 3;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 264, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), 0, default, 1.25f);
				dust.noGravity = true;
			}
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			
			
			if (projectile.timeLeft < 172)
			{
				projectile.ai[0]++;
				if (projectile.ai[0] > 2)
				{
					projectile.velocity.Y += Main.rand.NextFloat(-1f, 1f);
					projectile.velocity.X += Main.rand.NextFloat(-1f, 1f);
					projectile.ai[0] = 0;
				}
			
				for (int num363 = 0; num363 < 1; num363++)
				{
					float num364 = projectile.velocity.X / 3f * (float)num363;
					float num365 = projectile.velocity.Y / 3f * (float)num363;
					int num366 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 264, 0f, 0f, 0, new Color(255, 255, 255, 0), 1.5f);
					Main.dust[num366].position.X = projectile.Center.X - num364;
					Main.dust[num366].position.Y = projectile.Center.Y - num365;
					Main.dust[num366].velocity *= 0f;
					Main.dust[num366].noGravity = true;
				}
			}
			
			if (projectile.timeLeft < 150)
			{
				projectile.friendly = true;
				
				int num3;
				float num477 = projectile.Center.X;
				float num478 = projectile.Center.Y;
				float num479 = 500f;
				bool flag17 = false;

				for (int num480 = 0; num480 < 200; num480 = num3 + 1)
				{
					if (Main.npc[num480].CanBeChasedBy(projectile, false) && projectile.Distance(Main.npc[num480].Center) < num479 && Collision.CanHit(projectile.Center, 1, 1, Main.npc[num480].Center, 1, 1))
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
					float num488 = 3f;
					Vector2 vector38 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
					float num489 = num477 - vector38.X;
					float num490 = num478 - vector38.Y;
					float num491 = (float)Math.Sqrt((double)(num489 * num489 + num490 * num490));
					num491 = num488 / num491;
					num489 *= num491;
					num490 *= num491;

					projectile.velocity.X = (projectile.velocity.X * 20f + num489) / 21f;
					projectile.velocity.Y = (projectile.velocity.Y * 20f + num490) / 21f;
				}
			}
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}
	}
}