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
	public class HeartyClickerPro : ClickerProjectile
	{
		public bool tileCollide = false;
		
		public bool HasSpawnEffects
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}
		
		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 14;
			Projectile.height = 16;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 1200;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 50) * 1f;
		}
		
		public override void AI()
		{
			if (HasSpawnEffects)
			{
				SoundEngine.PlaySound(SoundID.Item87, Projectile.Center);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 8, 8, 90, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1f);
					dust.noGravity = true;
				}
				HasSpawnEffects = false;
			}
			
			Projectile.rotation = Projectile.velocity.X * -0.1f;
			Projectile.velocity.Y += 0.25f;
			if (tileCollide)
			{
				Projectile.velocity.X = 0f;
			}
			
			for (int k = 0; k < Main.maxPlayers; k++)
			{
				Player target = Main.player[k];
				if (target.active && target.statLife < target.statLifeMax2 && target.DistanceSQ(Projectile.Center) < (20 * 20))
				{
					target.HealLife(10);
					SoundEngine.PlaySound(SoundID.Item85, Projectile.position);
					for (int i = 0; i < 8; i++)
					{
						Dust dust = Dust.NewDustDirect(Projectile.Center, 8, 8, 90, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1f);
						dust.noGravity = true;
					}
					Projectile.Kill();
				}
			}
			if (Projectile.ai[1] >= 5f)
			{
				Projectile.Kill();
			}
			
			Projectile.alpha += Projectile.timeLeft < 20 ? 10 : 0;
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			tileCollide = true;
			return false;
		}
	}
}
