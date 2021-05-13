using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class ChlorophyteClickerPro : ClickerProjectile
	{
		public bool HasSpawnEffects
		{
			get => projectile.ai[0] == 1f;
			set => projectile.ai[0] = value ? 1f : 0f;
		}

		public override void SetDefaults()
		{
			projectile.width = 32;
			projectile.height = 32;
			projectile.aiStyle = -1;
			projectile.alpha = 150;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 180;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 45;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Poisoned, 300, false);
			target.AddBuff(BuffID.Venom, 300, false);
		}

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;
				Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 104);
			}

			projectile.rotation += projectile.velocity.X > 0f ? 0.1f : -0.1f;
			projectile.velocity *= 0.95f;
			if (projectile.timeLeft < 20)
			{
				projectile.alpha += 5;
			}
		}
	}
}