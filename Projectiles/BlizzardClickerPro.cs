using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ClickerClass.Projectiles
{
	public class BlizzardClickerPro : ClickerProjectile
	{
		public bool hoverDown = true;
		public int hoverAmount = 0;
		
		public bool Spawned
		{
			get => Projectile.ai[1] == 1f;
			set => Projectile.ai[1] = value ? 1f : 0f;
		}

		public int Timer
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 6;
		}
		
		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 54;
			Projectile.height = 54;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;

				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 24);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 92, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 0, default, 1f);
					dust.noGravity = true;
				}
			}

			Timer++;
			if (Timer > 15)
			{
				if (Main.myPlayer == Projectile.owner)
				{
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X + Main.rand.Next(-15, 16), Projectile.Center.Y), new Vector2(Main.rand.NextFloat(-0.4f, 0.4f), Main.rand.NextFloat(4f, 4.5f)), ModContent.ProjectileType<BlizzardClickerPro2>(), (int)(Projectile.damage * 0.5f), Projectile.knockBack, Projectile.owner);
				}
				Timer = 0;
			}
			
			// Hover up & down
			if (hoverDown)
			{
				hoverAmount++;
				if (hoverAmount % 15 == 0)
				{
					Projectile.position.Y++;
				}
				if (hoverAmount > 90)
				{
					hoverDown = false;
					hoverAmount = 0;
				}
			}
			else
			{
				hoverAmount++;
				if (hoverAmount % 15 == 0)
				{
					Projectile.position.Y--;
				}
				if (hoverAmount > 90)
				{
					hoverDown = true;
					hoverAmount = 0;
				}
			}
			
			if (Projectile.timeLeft < 30)
			{
				Projectile.alpha += 8;
			}
		}
		
		public override void PostAI()
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 6)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= 6)
			{
				Projectile.frame = 0;
			}
		}
	}
}
