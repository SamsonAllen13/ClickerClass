using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;

namespace ClickerClass.Projectiles
{
	public class CrystalClickerPro : ClickerProjectile
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
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10;
			Projectile.alpha = 255;
			Projectile.extraUpdates = 3;
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;

				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 28);

				int[] dusts = new int[] { 86, 88 };

				for (int k = 0; k < 15; k++)
				{
					for (int i = 0; i < dusts.Length; i++)
					{
						Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, dusts[i], Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 0, default, 1.25f);
						dust.noGravity = true;
					}
				}

				for (int u = 0; u < Main.maxNPCs; u++)
				{
					NPC target = Main.npc[u];
					if (target.CanBeChasedBy() && target.DistanceSQ(Projectile.Center) < 158 * 158)
					{
						target.AddBuff(BuffID.Confused, 180, false);

						for (int k = 0; k < 8; k++)
						{
							for (int i = 0; i < dusts.Length; i++)
							{
								int index = Dust.NewDust(target.position, target.width, target.height, dusts[i], 0f, 0f, 0, default(Color), 1f);
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
}