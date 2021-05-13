using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class CandleClickerPro : ClickerProjectile
	{
		public bool HasSpawnEffects
		{
			get => projectile.ai[0] == 1f;
			set => projectile.ai[0] = value ? 1f : 0f;
		}

		public bool CanSpawnLight
		{
			get => projectile.ai[1] == 1f;
			set => projectile.ai[1] = value ? 1f : 0f;
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.penetrate = -1;
			projectile.timeLeft = 600;
			projectile.alpha = 255;
			projectile.extraUpdates = 6;
		}

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;
				Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 74);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(projectile.Center - new Vector2(4), 8, 8, 55, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 255, default, 1.35f);
					dust.noGravity = true;
				}
			}

			if (CanSpawnLight)
			{
				projectile.extraUpdates = 0;
				for (int k = 0; k < 2; k++)
				{
					Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 55, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 255, default, 0.75f);
					dust.noGravity = true;
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (!CanSpawnLight)
			{
				projectile.timeLeft = 180;
				projectile.velocity = Vector2.Zero;
				projectile.netUpdate = true;
				CanSpawnLight = true;
			}
			return false;
		}
	}
}
