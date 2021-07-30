using Terraria;

namespace ClickerClass.Projectiles
{
	public class ArthursClickerPro : ClickerProjectile
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
			Projectile.penetrate = 1;
			Projectile.timeLeft = 10;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			crit = true;
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;
				for (int k = 0; k < 30; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 8, 8, 87, Projectile.velocity.X + Main.rand.NextFloat(-1f, 1f), Projectile.velocity.Y + Main.rand.NextFloat(-1f, 1f), 0, default, 1.25f);
					dust.noGravity = true;
				}
			}
		}
	}
}
