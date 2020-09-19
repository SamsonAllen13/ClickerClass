using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class ChlorophyteClickerPro : ModProjectile
	{
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
		
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[projectile.owner];
			damage = (int)(damage + (target.defense / 2));
			hitDirection = target.Center.X < player.Center.X ? -1 : 1;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Poisoned, 300, false);
			target.AddBuff(BuffID.Venom, 300, false);
		}

		public override void AI()
		{
			projectile.rotation += projectile.velocity.X > 0f ? 0.1f : -0.1f;
			projectile.velocity *= 0.95f;
			if (projectile.timeLeft < 20)
			{
				projectile.alpha += 5;
			}
		}
	}
}