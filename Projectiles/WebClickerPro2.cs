using ClickerClass.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class WebClickerPro2 : ClickerProjectile
	{
		public bool LeftFacing => projectile.ai[0] == 1f;

		public override void SetDefaults()
		{
			projectile.width = 78;
			projectile.height = 76;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 40;
			projectile.tileCollide = false;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<Stunned>(), 60, false);
		}

		public override void AI()
		{
			projectile.rotation += LeftFacing ? 0.0075f : -0.0075f;

			if (projectile.timeLeft < 20)
			{
				projectile.alpha += 10;
			}
		}
	}
}
