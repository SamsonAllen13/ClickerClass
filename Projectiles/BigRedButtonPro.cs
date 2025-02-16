using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class BigRedButtonPro : ClickerProjectile
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 7;

			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Fire);
			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Explosive);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 98;
			Projectile.height = 98;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 60;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 60;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 50) * (0.08f * Projectile.timeLeft);
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.OnFire3, 300, false);
		}

		public override void PostAI()
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 4)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= 12)
			{
				Projectile.Kill();
				return;
			}
		}
	}
}