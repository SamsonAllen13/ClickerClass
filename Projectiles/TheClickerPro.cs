using ClickerClass.Items.Weapons.Clickers;
using Terraria;
using Terraria.GameContent.Drawing;

namespace ClickerClass.Projectiles
{
	public class TheClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.aiStyle = -1;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			//Projectile is spawned with 1 damage, so it will always guarantee dealing 1 damage, and we subtract it
			int fixedDamage = (int)(target.lifeMax * TheClicker.AdditionalDamageLifeRatio / 100f);
			modifiers.FinalDamage.Flat += fixedDamage - 1; //using FinalDamage more or less guarantees killing enemies in 100 hits as it ignores armor/endurance
			modifiers.DamageVariationScale *= 0f;
		}

		public override void OnKill(int timeLeft)
		{
			// Silver Bullet sparkles
			ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.SilverBulletSparkle, new ParticleOrchestraSettings
			{
				PositionInWorld = Projectile.Center
			}, Projectile.owner);
		}
	}
}
