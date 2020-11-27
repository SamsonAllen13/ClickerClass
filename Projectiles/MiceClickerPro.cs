using ClickerClass.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class MiceClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.penetrate = 1;
			projectile.alpha = 255;
			projectile.timeLeft = 600;
			projectile.extraUpdates = 1;
			projectile.tileCollide = false;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];

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
				int num366 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, ModContent.DustType<MiceDust>(), 0f, 0f, 0, default(Color), 1f);
				Main.dust[num366].position.X = projectile.Center.X - num364;
				Main.dust[num366].position.Y = projectile.Center.Y - num365;
				Main.dust[num366].velocity *= 0f;
				Main.dust[num366].noGravity = true;
			}

			if (projectile.timeLeft < 540)
			{
				projectile.friendly = true;
				projectile.extraUpdates = 4;

				if (Main.myPlayer == projectile.owner)
				{
					float num477 = projectile.Center.X;
					float num478 = projectile.Center.Y;
					float num479 = 1000f;
					bool flag17 = false;

					float num481 = Main.MouseWorld.X;
					float num482 = Main.MouseWorld.Y;
					float num483 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num481) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num482);
					if (num483 < num479)
					{
						num479 = num483;
						num477 = num481;
						num478 = num482;
						flag17 = true;
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
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, ModContent.DustType<MiceDust>(), Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1f);
				dust.noGravity = true;
			}
		}
	}
}