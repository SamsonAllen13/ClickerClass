using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;

namespace ClickerClass.Projectiles
{
	public class DarkClickerPro : ClickerProjectile
	{
		public bool Spawned
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.extraUpdates = 3;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			for (int i = 0; i < 15; i++)
			{
				int index = Dust.NewDust(target.position, target.width, target.height, 14, 0f, 0f, 75, default(Color), 1.5f);
				Dust dust = Main.dust[index];
				dust.noGravity = true;
				dust.velocity *= 0.75f;
				int x = Main.rand.Next(-50, 51);
				int y = Main.rand.Next(-50, 51);
				dust.position.X += x;
				dust.position.Y += y;
				dust.velocity.X = -x * 0.075f;
				dust.velocity.Y = -y * 0.075f;
			}
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;
				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 70);
				for (int k = 0; k < 25; k++)
				{
					Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 14, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 75, default, 1.5f);
					dust.noGravity = true;
				}
			}
		}
	}
}
