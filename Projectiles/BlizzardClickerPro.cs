using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class BlizzardClickerPro : ClickerProjectile
	{
		public bool hoverDown = true;
		public int hoverAmount = 0;
		
		public bool Spawned
		{
			get => projectile.ai[1] == 1f;
			set => projectile.ai[1] = value ? 1f : 0f;
		}

		public int Timer
		{
			get => (int)projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[projectile.type] = 6;
		}
		
		public override void SetDefaults()
		{
			projectile.width = 54;
			projectile.height = 54;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;

				Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 24);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(projectile.Center - new Vector2(4), 8, 8, 92, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 0, default, 1f);
					dust.noGravity = true;
				}
			}

			Timer++;
			if (Timer > 15)
			{
				if (Main.myPlayer == projectile.owner)
				{
					Projectile.NewProjectile(new Vector2(projectile.Center.X + Main.rand.Next(-15, 16), projectile.Center.Y), new Vector2(Main.rand.NextFloat(-0.4f, 0.4f), Main.rand.NextFloat(4f, 4.5f)), ModContent.ProjectileType<BlizzardClickerPro2>(), (int)(projectile.damage * 0.5f), projectile.knockBack, projectile.owner);
				}
				Timer = 0;
			}
			
			// Hover up & down
			if (hoverDown)
			{
				hoverAmount++;
				if (hoverAmount % 15 == 0)
				{
					projectile.position.Y++;
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
					projectile.position.Y--;
				}
				if (hoverAmount > 90)
				{
					hoverDown = true;
					hoverAmount = 0;
				}
			}
			
			if (projectile.timeLeft < 30)
			{
				projectile.alpha += 8;
			}
		}
		
		public override void PostAI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter > 6)
			{
				projectile.frame++;
				projectile.frameCounter = 0;
			}
			if (projectile.frame >= 6)
			{
				projectile.frame = 0;
			}
		}
	}
}
