using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class LordsClickerPro : ClickerProjectile
	{
		public bool Spawned
		{
			get => projectile.ai[0] == 1f;
			set => projectile.ai[0] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[projectile.type] = 7;
		}

		public override void SetDefaults()
		{
			projectile.width = 196;
			projectile.height = 196;
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
			return new Color(255, 255, 255, 0) * (0.08f * projectile.timeLeft);
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			damage = (int)(damage * 2.5f);
			crit = true;
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;
				Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 88);
			}

			projectile.frameCounter++;
			if (projectile.frameCounter > 3)
			{
				projectile.frame++;
				projectile.frameCounter = 0;
			}
			if (projectile.frame >= 12)
			{
				projectile.Kill();
			}
		}
	}
}
