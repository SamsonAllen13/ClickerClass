using Terraria;

namespace ClickerClass.Projectiles
{
	public class ArthursClickerPro : ClickerProjectile
	{
		public bool Spawned
		{
			get => projectile.ai[0] == 1f;
			set => projectile.ai[0] = value ? 1f : 0f;
		}

		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = -1;
			projectile.alpha = 255;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 10;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
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
					Dust dust = Dust.NewDustDirect(projectile.Center, 8, 8, 87, projectile.velocity.X + Main.rand.NextFloat(-1f, 1f), projectile.velocity.Y + Main.rand.NextFloat(-1f, 1f), 0, default, 1.25f);
					dust.noGravity = true;
				}
			}
		}
	}
}
