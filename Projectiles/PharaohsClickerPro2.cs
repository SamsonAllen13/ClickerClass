using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class PharaohsClickerPro2 : ClickerProjectile
	{
		public bool HasSpawnEffects
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Arcane);
			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Earth);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.aiStyle = -1;
			Projectile.penetrate = 1;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.timeLeft = 180;
			Projectile.extraUpdates = 5;
		}

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				SoundEngine.PlaySound(SoundID.Item42, Projectile.Center);
				float num102 = 20f;
				int num103 = 0;
				while ((float)num103 < num102)
				{
					Vector2 vector12 = Vector2.UnitX * 0f;
					vector12 += -Vector2.UnitY.RotatedBy((double)((float)num103 * (6.28318548f / num102)), default(Vector2)) * new Vector2(8f, 8f);
					vector12 = vector12.RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
					int num104 = Dust.NewDust(Projectile.Center, 0, 0, 57, 0f, 0f, 255, default(Color), 0.75f);
					Main.dust[num104].noGravity = true;
					Main.dust[num104].position = Projectile.Center + vector12;
					Main.dust[num104].velocity = Projectile.velocity * 0f + vector12.SafeNormalize(Vector2.UnitY) * 1f;
					int num = num103;
					num103 = num + 1;
				}
				HasSpawnEffects = false;
			}

			int index = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 57, 0f, 0f, 255, default(Color), 1.25f);
			Dust dust = Main.dust[index];
			dust.position.X = Projectile.Center.X;
			dust.position.Y = Projectile.Center.Y;
			dust.velocity *= 0f;
			dust.noGravity = true;
		}

		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 8, 8, 57, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 255, default, 1f);
				dust.noGravity = true;
			}
		}
	}
}
