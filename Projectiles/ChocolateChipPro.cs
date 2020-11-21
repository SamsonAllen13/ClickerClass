using Terraria;

namespace ClickerClass.Projectiles
{
	public class ChocolateChipPro : ClickerProjectile
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 60;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 20;
		}

		public override void AI()
		{
			projectile.frame = (int)(projectile.ai[0]);
			projectile.velocity *= 0.9f;
			projectile.rotation += projectile.velocity.X > 0f ? 0.08f : 0.08f;
			if (projectile.timeLeft < 20)
			{
				projectile.alpha += 8;
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 8; k++)
			{
				int dust = Dust.NewDust(projectile.position, (int)(projectile.width * 0.5f), (int)(projectile.height * 0.5f), 22, Main.rand.Next((int)-2f, (int)2f), Main.rand.Next((int)-2f, (int)2f), 125, default, 1.25f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}