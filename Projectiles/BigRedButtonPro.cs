using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class BigRedButtonPro : ClickerProjectile
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[projectile.type] = 7;
		}

		public override void SetDefaults()
		{
			projectile.width = 98;
			projectile.height = 98;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.timeLeft = 60;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 60;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 50) * (0.08f * projectile.timeLeft);
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Oiled, 300, false);
			target.AddBuff(BuffID.OnFire, 300, false);
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