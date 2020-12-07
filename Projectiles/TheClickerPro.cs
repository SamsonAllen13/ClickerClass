using Terraria;
using Terraria.Graphics.Shaders;

namespace ClickerClass.Projectiles
{
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
		
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			damage = (int)(target.lifeMax * 0.01f);
		}

		public override void Kill(int timeLeft)
		{
			for (int l = 0; l < 7; l++)
			{
				int dustType = 86 + l;
				for (int k = 0; k < 5; k++)
				{
					if (dustType == 91)
					{
						Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 92, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 0, default, 1f);
						dust.shader = GameShaders.Armor.GetSecondaryShader(70, Main.LocalPlayer);
						dust.noGravity = true;
					}
					else
					{
						Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, dustType, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 0, default, 1f);
						dust.noGravity = true;
					}
				}
			}
		}
	}
}