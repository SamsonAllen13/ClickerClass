using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class LihzarhdClickerPro : ClickerProjectile
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[projectile.type] = 12;
		}

		public override void SetDefaults()
		{
			projectile.width = 128;
			projectile.height = 128;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.timeLeft = 60;
			projectile.friendly = true;
			projectile.tileCollide = false;

			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 5;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 0) * (0.04f * projectile.timeLeft);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Oiled, 180, false);
			target.AddBuff(BuffID.OnFire, 180, false);
		}

		public override void PostAI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter > 4)
			{
				projectile.frame++;
				projectile.frameCounter = 0;
			}
			if (projectile.frame >= 12)
			{
				projectile.Kill();
				return;
			}
		}
	}
}