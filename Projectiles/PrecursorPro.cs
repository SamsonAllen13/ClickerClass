using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class PrecursorPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			isClickerProj = true;
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = -1;
			projectile.alpha = 255;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 40;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Oiled, 300, false);
			target.AddBuff(BuffID.OnFire, 300, false);
		}

		public override void AI()
		{
			if (projectile.timeLeft <= 10)
			{
				projectile.friendly = true;
				if (projectile.ai[0] < 1f)
				{
					projectile.ai[0] += 1f;
					for (int k = 0; k < 8; k++)
					{
						Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 174, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1.25f);
						dust.noGravity = true;
					}
				}
			}
		}
	}
}