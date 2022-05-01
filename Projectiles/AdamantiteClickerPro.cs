using Terraria;
using Terraria.ID;
using Terraria.Audio;

namespace ClickerClass.Projectiles
{
	public class AdamantiteClickerPro : ClickerProjectile
	{
		public bool Spawned
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = -1;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			damage *= 2;
			crit = true;
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;

				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 71);
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 8; k++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 90, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 0, default, 1.25f);
				dust.noGravity = true;
			}
		}
	}
}
