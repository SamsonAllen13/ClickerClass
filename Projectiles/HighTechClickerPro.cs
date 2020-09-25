using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;

namespace ClickerClass.Projectiles
{
	public class HighTechClickerPro : ClickerProjectile
	{
		List<int> targets = new List<int>();
		public int timer = 0;

		public override void SetDefaults()
		{
			isClickerProj = true;
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.alpha = 255;
			projectile.friendly = true;
			projectile.timeLeft = 600;
			projectile.extraUpdates = 100;
			
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
		}

		public override bool? CanHitNPC(NPC target)
		{
			if (targets.Contains(target.whoAmI))
			{
				return false;
			}
			return base.CanHitNPC(target);
		}
		
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[projectile.owner];
			damage = (int)(damage + (target.defense / 2));
			hitDirection = target.Center.X < player.Center.X ? -1 : 1;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.velocity = Vector2.Zero;
			targets.Add(target.whoAmI);
			if (targets.Count >= 8)
			{
				projectile.Kill();
			}
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			for (int num363 = 0; num363 < 1; num363++)
			{
				float num364 = projectile.velocity.X / 3f * (float)num363;
				float num365 = projectile.velocity.Y / 3f * (float)num363;
				int num366 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 229, 0f, 0f, 0, default(Color), 1.15f);
				Main.dust[num366].position.X = projectile.Center.X - num364;
				Main.dust[num366].position.Y = projectile.Center.Y - num365;
				Main.dust[num366].velocity *= 0f;
				Main.dust[num366].noGravity = true;
			}
			
			if (projectile.timeLeft < 580)
			{
				if (projectile.timeLeft >= 600)
				{
					for (int k = 0; k < 6; k++)
					{
						int num367 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 229, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 125, default(Color), 1.15f);
						Main.dust[num367].noGravity = true;
					}
				}

				timer++;
				if (timer > 2)
				{
					projectile.velocity.Y += Main.rand.NextFloat(-0.75f, 0.75f);
					projectile.velocity.X += Main.rand.NextFloat(-0.75f, 0.75f);
					timer = 0;
				}

				int num3;
				float num477 = player.Center.X;
				float num478 = player.Center.Y;
				float num479 = 750f;
				bool flag17 = false;

				for (int num480 = 0; num480 < 200; num480 = num3 + 1)
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
					float num488 = 2.5f;

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
				else
				{
					projectile.velocity = Vector2.Zero;
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 10; k++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 229, Main.rand.Next((int)-5f, (int)5f), Main.rand.Next((int)-5f, (int)5f), 75, default(Color), 0.75f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}