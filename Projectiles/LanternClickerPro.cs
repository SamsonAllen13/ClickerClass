using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;

namespace ClickerClass.Projectiles
{
	public class LanternClickerPro : ClickerProjectile
	{
		public bool HasSpawnEffects
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public bool CanSpawnLight
		{
			get => Projectile.ai[1] == 1f;
			set => Projectile.ai[1] = value ? 1f : 0f;
		}
		
		public virtual int ExplosionHitboxSize => 80;

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
			Projectile.alpha = 255;
			Projectile.extraUpdates = 6;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 60;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Oiled, 60, false);
			target.AddBuff(BuffID.OnFire, 60, false);
		}

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;
				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 14);
				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 74);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 55, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 255, default, 1.35f);
					dust.noGravity = true;
				}
			}

			if (CanSpawnLight)
			{
				Projectile.extraUpdates = 0;
				for (int k = 0; k < 3; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 174, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 255, default, 0.75f);
					dust.noGravity = true;
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (!CanSpawnLight)
			{
				Projectile.timeLeft = 180;
				Projectile.velocity = Vector2.Zero;
				Projectile.netUpdate = true;
				Projectile.friendly = true;
				CanSpawnLight = true;
				
				if (Projectile.owner == Main.myPlayer)
				{
					Projectile.velocity.X = 0f;
					Projectile.velocity.Y = 0f;
					Projectile.tileCollide = false;
					Projectile.alpha = 255;
					Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
					Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
					Projectile.width = ExplosionHitboxSize;
					Projectile.height = ExplosionHitboxSize;
					Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
					Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
				}
			}
			return false;
		}
	}
}
