using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;

namespace ClickerClass.Projectiles
{
	public class ChocolateChipPro : ClickerProjectile
	{
		public int Frame
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public bool HasSpawnEffects
		{
			get => Projectile.ai[1] == 1f;
			set => Projectile.ai[1] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 60;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 20;
		}

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;

				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 112);
				for (int k = 0; k < 20; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 22, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 125, default, 1.5f);
					dust.noGravity = true;
					dust.noLight = true;
				}
			}

			Projectile.frame = Frame;
			Projectile.velocity *= 0.9f;
			Projectile.rotation += Projectile.velocity.X > 0f ? 0.08f : 0.08f;
			if (Projectile.timeLeft < 20)
			{
				Projectile.alpha += 8;
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 8; k++)
			{
				int dust = Dust.NewDust(Projectile.position, (int)(Projectile.width * 0.5f), (int)(Projectile.height * 0.5f), 22, Main.rand.Next((int)-2f, (int)2f), Main.rand.Next((int)-2f, (int)2f), 125, default, 1.25f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}
