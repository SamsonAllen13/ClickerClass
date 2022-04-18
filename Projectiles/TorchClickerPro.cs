using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class TorchClickerPro : ClickerProjectile
	{
		public int Timer
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public bool Spawned
		{
			get => Projectile.localAI[0] == 1f;
			set => Projectile.localAI[0] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.alpha = 255;
			Projectile.timeLeft = 300;
			Projectile.extraUpdates = 2;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override void AI()
		{
			int index = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 6, 0f, 0f, 0, default(Color), 1.5f);
			Dust dust = Main.dust[index];
			dust.position.X = Projectile.Center.X;
			dust.position.Y = Projectile.Center.Y;
			dust.velocity *= 0f;
			dust.noGravity = true;
			
			if (!Spawned)
			{
				Spawned = true;

				float max = 30f;
				int i = 0;
				while (i < max)
				{
					Vector2 vector2 = Vector2.Zero;
					vector2 += -Vector2.UnitY.RotatedBy(i * (MathHelper.TwoPi / max)) * new Vector2(2f, 2f);
					vector2 = vector2.RotatedBy(Projectile.velocity.ToRotation(), default(Vector2));
					int index2 = Dust.NewDust(Projectile.Center, 0, 0, 6, 0f, 0f, 0, default(Color), 1.5f);
					Dust dust2 = Main.dust[index2];
					dust2.noGravity = true;
					dust2.position = Projectile.Center + vector2;
					dust2.velocity = vector2.SafeNormalize(Vector2.UnitY) * 2f;
					i++;
				}
			}
		}
	}
}
