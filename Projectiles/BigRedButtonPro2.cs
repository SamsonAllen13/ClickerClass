using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ClickerClass.Projectiles
{
	public class BigRedButtonPro2 : ClickerProjectile
	{
		public bool pulseShift = false;
		
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 4;
		}
		
		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 54;
			Projectile.height = 54;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
			Projectile.extraUpdates = 4;
			Projectile.tileCollide = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 50) * Projectile.ai[1];
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Oiled, 300, false);
			target.AddBuff(BuffID.OnFire, 300, false);
		}

		public override void AI()
		{
			Projectile.ai[0]++;
			if (Projectile.ai[0] < 90)
			{
				Projectile.velocity.Y /= 1.0065f;
				
				if (Projectile.ai[0] % 15 == 0)
				{
					Projectile.velocity.Y += 1.05f;
				}
				
				for (int num363 = 0; num363 < 3; num363++)
				{
					float num364 = Projectile.velocity.X / 3f * (float)num363;
					float num365 = Projectile.velocity.Y / 3f * (float)num363;
					int num366 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 55, 0f, 0f, 75, default(Color), 1f);
					Main.dust[num366].position.X = Projectile.Center.X - num364;
					Main.dust[num366].position.Y = Projectile.Center.Y - num365;
					Main.dust[num366].velocity *= 0f;
					Main.dust[num366].noGravity = true;
				}
			}
			else
			{
				if (Projectile.ai[1] < 1f)
				{
					SoundEngine.PlaySound(2, (int)Projectile.Center.X, (int)Projectile.Center.Y, 110);
					for (int k = 0; k < 10; k++)
					{
						Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 55, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 75, default, 1f);
						dust.noGravity = true;
					}
					Projectile.ai[1] = 1f;
				}
				Projectile.velocity = Vector2.Zero;
				Projectile.friendly = true;
				Projectile.extraUpdates = 0;
				Projectile.rotation += 0.25f;
				Projectile.scale += !pulseShift ? 0.04f : -0.08f;
				if (Projectile.scale > 1.5f)
				{
					pulseShift = true;
					for (int k = 0; k < 6; k++)
					{
						Vector2 spread = new Vector2(-0.5f, -3f);
						if (k == 1){spread = new Vector2(-0.25f, -2.5f);}
						if (k == 2){spread = new Vector2(-0.25f, -3.5f);}
						if (k == 3){spread = new Vector2(0.25f, -3.5f);}
						if (k == 4){spread = new Vector2(0.25f, -2.5f);}
						if (k == 5){spread = new Vector2(0.5f, -3f);}
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, spread.X, spread.Y, ModContent.ProjectileType<BigRedButtonPro3>(), (int)(Projectile.damage * 0.5f), 0f, Projectile.owner);
					}
				}
				if (Projectile.scale <= 0f)
				{
					Projectile.Kill();
				}
			}
		}
		
		public override void PostAI()
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 4)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= 4)
			{
				Projectile.frame = 0;
				return;
			}
		}
	}
}