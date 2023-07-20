using ClickerClass.Items.Weapons.Clickers;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class HeartyClickerPro : ClickerProjectile
	{
		public int activeTimer = 0;
		public bool tileCollide = false;

		public bool Spawned
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public int HeartCount
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 14;
			Projectile.height = 16;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 1800;
			Projectile.ignoreWater = true;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(activeTimer, activeTimer, activeTimer, 50) * 1f;
		}

		public override void AI()
		{
			if (!Spawned)
			{
				SoundEngine.PlaySound(SoundID.Item87, Projectile.Center);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 8, 8, 90, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1f);
					dust.noGravity = true;
				}
				Spawned = true;
			}

			Projectile.rotation = Projectile.velocity.X * -0.1f;
			Projectile.velocity.Y += 0.25f;
			if (tileCollide)
			{
				Projectile.velocity.X = 0f;
			}

			//Takes about 1 second to become active
			if (activeTimer >= 255)
			{
				activeTimer = 255;
				for (int k = 0; k < Main.maxPlayers; k++)
				{
					Player target = Main.player[k];
					if (target.active && target.statLife < target.statLifeMax2 && target.DistanceSQ(Projectile.Center) < (20 * 20))
					{
						target.HealLife(10);
						SoundEngine.PlaySound(SoundID.Item85, Projectile.position);
						for (int i = 0; i < 8; i++)
						{
							Dust dust = Dust.NewDustDirect(Projectile.Center, 8, 8, 90, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1f);
							dust.noGravity = true;
						}
						Projectile.Kill();
					}
				}
			}
			else
			{
				activeTimer += 4;
			}

			if (HeartCount >= HeartyClicker.HeartMaxAmount)
			{
				Projectile.Kill();
			}

			Projectile.alpha += Projectile.timeLeft < 20 ? 10 : 0;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			tileCollide = true;
			return false;
		}
	}
}
