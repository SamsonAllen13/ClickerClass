using ClickerClass.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class WebClickerPro2 : ClickerProjectile
	{
		public bool LeftFacing => Projectile.ai[0] == 1f;

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 78;
			Projectile.height = 76;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 40;
			Projectile.tileCollide = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<Stunned>(), 60, false);
		}

		public override void AI()
		{
			Projectile.rotation += LeftFacing ? 0.0075f : -0.0075f;

			if (Projectile.timeLeft < 20)
			{
				Projectile.alpha += 10;
			}
		}
	}
}
