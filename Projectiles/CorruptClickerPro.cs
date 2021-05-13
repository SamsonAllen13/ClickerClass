using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class CorruptClickerPro : ClickerProjectile
	{
		public bool Spawned
		{
			get => projectile.ai[0] == 1f;
			set => projectile.ai[0] = value ? 1f : 0f;
		}

		public override void SetDefaults()
		{
			projectile.width = 250;
			projectile.height = 250;
			projectile.penetrate = -1;
			projectile.timeLeft = 10;
			projectile.alpha = 255;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.extraUpdates = 3;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Oiled, 180, false);
			target.AddBuff(BuffID.CursedInferno, 180, false);

			for (int i = 0; i < 15; i++)
			{
				int index = Dust.NewDust(target.position, target.width, target.height, 163, 0f, 0f, 0, default(Color), 1.5f);
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

				Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 74);

				for (int k = 0; k < 30; k++)
				{
					Dust dust = Dust.NewDustDirect(projectile.Center - new Vector2(4), 8, 8, 163, Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(-10f, 10f), 0, default, 1.65f);
					dust.noGravity = true;
				}
			}
		}
	}
}
