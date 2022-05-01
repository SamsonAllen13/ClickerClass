using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;

namespace ClickerClass.Projectiles
{
	public class SinisterClickerPro : ClickerProjectile
	{
		public bool Spawned
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 10;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.extraUpdates = 3;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[Projectile.owner];
			player.HealLife(5);
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;

				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 112);
				for (int i = 0; i < 15; i++)
				{
					int index = Dust.NewDust(Projectile.Center, 4, 4, 5, 0f, 0f, 75, default(Color), 1.5f);
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
		}
	}
}
