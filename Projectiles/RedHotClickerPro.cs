using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class RedHotClickerPro : ClickerProjectile
	{
		public bool Spawned
		{
			get => projectile.ai[0] == 1f;
			set => projectile.ai[0] = value ? 1f : 0f;
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.penetrate = -1;
			projectile.timeLeft = 10;
			projectile.alpha = 255;
			projectile.extraUpdates = 3;
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;

				Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 74);

				for (int k = 0; k < 30; k++)
				{
					Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 174, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 125, default, 1.25f);
					dust.noGravity = true;
				}

				for (int u = 0; u < Main.maxNPCs; u++)
				{
					NPC target = Main.npc[u];
					if (target.CanBeChasedBy() && target.DistanceSQ(projectile.Center) < 158 * 158)
					{
						target.AddBuff(BuffID.Oiled, 180, false);
						target.AddBuff(BuffID.OnFire, 180, false);

						for (int i = 0; i < 15; i++)
						{
							int index = Dust.NewDust(target.position, target.width, target.height, 174, 0f, 0f, 100, default(Color), 1f);
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
	}
}
