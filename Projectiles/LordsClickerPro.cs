using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class LordsClickerPro : ClickerProjectile
	{
		public bool Spawned
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 7;

			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Celestial);
			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Explosive);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 196;
			Projectile.height = 196;
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
			return new Color(255, 255, 255, 0) * (0.08f * Projectile.timeLeft);
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			modifiers.SourceDamage += 1.5f;
			modifiers.SetCrit();
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;
				SoundEngine.PlaySound(SoundID.Item88, Projectile.Center);
			}

			Projectile.frameCounter++;
			if (Projectile.frameCounter > 3)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= 12)
			{
				Projectile.Kill();
			}
		}
	}
}
