using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class StingingClickerPro : ClickerProjectile
	{
		public int timeMax = 300;
		
		public bool Spawned
		{
			get => Projectile.ai[2] == 1f;
			set => Projectile.ai[2] = value ? 1f : 0f;
		}
		
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 4;
			
			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Poison);
		}

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = 36;
			Projectile.scale = 1.2f;
			Projectile.penetrate = 1;
			Projectile.timeLeft = timeMax;
			AIType = ProjectileID.Bee;
		}
		
		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;
				SoundEngine.PlaySound(SoundID.Item17, Projectile.Center);
				for (int k = 0; k < 10; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 138, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 125, default, 1.5f);
					dust.noGravity = true;
					dust.noLight = true;
				}
			}
			
			if (Projectile.timeLeft < timeMax - 30)
			{
				Projectile.friendly = true;
			}

		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}

		public override void PostAI()
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 3)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= Main.projFrames[Projectile.type])
			{
				Projectile.frame = 0;
			}
		}
	}
}
