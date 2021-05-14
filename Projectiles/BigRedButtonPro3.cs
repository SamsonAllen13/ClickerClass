using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class BigRedButtonPro3 : ClickerProjectile
	{
		public bool pulseShift = false;
		
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[projectile.type] = 4;
		}
		
		public override void SetDefaults()
		{
			projectile.width = 36;
			projectile.height = 36;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
			projectile.extraUpdates = 4;
			projectile.tileCollide = false;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 60;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 50) * projectile.ai[1];
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Oiled, 300, false);
			target.AddBuff(BuffID.OnFire, 300, false);
		}

		public override void AI()
		{
			projectile.ai[0]++;
			if (projectile.ai[0] < 90)
			{
				projectile.velocity.Y /= 1.0065f;
				
				if (projectile.ai[0] % 15 == 0)
				{
					projectile.velocity.Y += 1.05f;
				}
				
				for (int num363 = 0; num363 < 3; num363++)
				{
					float num364 = projectile.velocity.X / 3f * (float)num363;
					float num365 = projectile.velocity.Y / 3f * (float)num363;
					int num366 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 55, 0f, 0f, 75, default(Color), 0.75f);
					Main.dust[num366].position.X = projectile.Center.X - num364;
					Main.dust[num366].position.Y = projectile.Center.Y - num365;
					Main.dust[num366].velocity *= 0f;
					Main.dust[num366].noGravity = true;
				}
			}
			else
			{
				if (projectile.ai[1] < 1f)
				{
					Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 118);
					for (int k = 0; k < 10; k++)
					{
						Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 55, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 75, default, 1f);
						dust.noGravity = true;
					}
					projectile.ai[1] = 1f;
				}
				projectile.velocity = Vector2.Zero;
				projectile.friendly = true;
				projectile.extraUpdates = 0;
				projectile.rotation += 0.25f;
				projectile.scale += !pulseShift ? 0.04f : -0.08f;
				if (projectile.scale > 1.5f)
				{
					pulseShift = true;
				}
				if (projectile.scale <= 0f)
				{
					projectile.Kill();
				}
			}
		}
		
		public override void PostAI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter > 4)
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