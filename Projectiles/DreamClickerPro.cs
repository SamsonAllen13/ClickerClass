using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace ClickerClass.Projectiles
{
	public class DreamClickerPro : ClickerProjectile
	{
		public int decide = 0;
		public int decide2 = 0;
		public bool velocityYShift = false;
		public bool velocityXShift = false;
		
		public bool HasSpawnEffects
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}
		
		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 42;
			Projectile.height = 42;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 55;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 50) * (0.75f * Projectile.Opacity);
		}
		
		public override void AI()
		{
			decide = Projectile.ai[1] < 2 ? 1 : -1;
			decide2 = (Projectile.ai[1] == 0 || Projectile.ai[1] == 2) ? 1 : 2;
			
			if (HasSpawnEffects)
			{
				SoundEngine.PlaySound(SoundID.Item43, Projectile.Center);
				SoundEngine.PlaySound(SoundID.Item17, Projectile.Center);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 8, 8, 57, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 255, default, 1.25f);
					dust.noGravity = true;
				}
				HasSpawnEffects = false;
			}
			
			Projectile.velocity.Y += !velocityYShift ? 0.25f * decide2 : -0.6f * decide2;
			if (Projectile.velocity.Y > 6f * decide2)
			{
				velocityYShift = true;
			}
			
			Projectile.velocity.X += !velocityXShift ? ((0.025f * decide2) + 0.35f) * decide : -((0.05f * decide2) + 0.55f) * decide;
			if (decide == 1)
			{
				if (Projectile.velocity.X > 7f + (0.5f * decide2))
				{
					velocityXShift = true;
				}
			}
			else
			{
				if (Projectile.velocity.X < -7f - (0.5f * decide2))
				{
					velocityXShift = true;
				}
			}
			
			Projectile.rotation += 0.5f * decide;
			Projectile.alpha += Projectile.timeLeft < 20 ? 10 : 0;
		}
	}
}
