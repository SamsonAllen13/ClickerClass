using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Projectiles
{
	public class PrecursorPro : ClickerProjectile
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Fire);
			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Earth);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 16;
			Projectile.height = 22;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 30;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 60;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 50) * Projectile.ai[1];
		}

		public override void AI()
		{
			Projectile.scale += Projectile.ai[0] == 1f ? -0.015f : 0.075f;
			Projectile.ai[1] += Projectile.ai[0] == 1f ? -0.025f : 0.2f;
			if (Projectile.ai[1] >= 1f)
			{
				Projectile.ai[0] = 1f;
			}
		}
	}
}
