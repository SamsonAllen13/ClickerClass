using Terraria;
using Terraria.Graphics.Shaders;

namespace ClickerClass.Projectiles
{
	//TODO orphaned?
	public class TheClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 40;
			projectile.aiStyle = -1;
			projectile.alpha = 255;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 10;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
		}

		public override void Kill(int timeLeft)
		{
			Player player = Main.player[projectile.owner];

			for (int k = 0; k < 8; k++)
			{
				Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 90, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 75, default, 1f);
				dust.noGravity = true;
			}
			for (int k = 0; k < 8; k++)
			{
				Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 92, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 75, default, 1f);
				dust.shader = GameShaders.Armor.GetSecondaryShader(70, Main.LocalPlayer);
				dust.noGravity = true;
			}
			for (int k = 0; k < 8; k++)
			{
				Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 87, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 75, default, 1f);
				dust.noGravity = true;
			}
			for (int k = 0; k < 8; k++)
			{
				Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 89, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 75, default, 1f);
				dust.noGravity = true;
			}
			for (int k = 0; k < 8; k++)
			{
				Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 92, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 75, default, 1f);
				dust.noGravity = true;
			}
			for (int k = 0; k < 8; k++)
			{
				Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 88, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 75, default, 1f);
				dust.noGravity = true;
			}
			for (int k = 0; k < 8; k++)
			{
				Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 86, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 75, default, 1f);
				dust.noGravity = true;
			}
		}
	}
}