using ClickerClass.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class CosmicClickerPro5 : ClickerProjectile
	{
		public int shockAmount = 0;
		
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
			Main.projFrames[Projectile.type] = 4;
		}
		
		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 100;
			Projectile.height = 100;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
			Projectile.alpha = 255;
			Projectile.tileCollide = false;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 0) * Projectile.Opacity;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			
			if (!Spawned)
			{
				Spawned = true;

				SoundEngine.PlaySound(SoundID.Item43.WithPitchOffset(-0.2f), Projectile.Center);
				for (int k = 0; k < 10; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 226, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 0, default, 1.5f);
					dust.noGravity = true;
				}
			}
			
			if (Projectile.ai[0] == 0f)
			{
				Projectile.alpha = 0;
				Projectile.scale = 0.1f;
				Projectile.ai[0] = 1f;
			}
			if (Projectile.scale < 1f)
			{
				Projectile.scale += 0.05f;
			}
			Projectile.velocity *= 0.95f;
			
			MousePlayer mousePlayer = Main.player[Projectile.owner].GetModPlayer<MousePlayer>();
			if (mousePlayer.TryGetMousePosition(out Vector2 mouseWorld))
			{
				Vector2 toMouse = mouseWorld - Projectile.Center;

				Timer++;
				if (shockAmount < 6)
				{
					if (Timer >= 60)
					{
						Timer = 30;
						SoundEngine.PlaySound(SoundID.Item94, Projectile.Center);
						if (Main.myPlayer == Projectile.owner)
						{
							for (int k = 0; k < 10; k++)
							{
								int dust = Dust.NewDust(Projectile.Center, 10, 10, 226, Main.rand.Next((int)-5f, (int)5f), Main.rand.Next((int)-5f, (int)5f), 0, default(Color), 1.25f);
								Main.dust[dust].noGravity = true;
							}
			
							var source = Projectile.GetSource_FromThis();
							Vector2 toMouseNormalized = toMouse.SafeNormalize(Vector2.Zero);
							Vector2 position = Projectile.Center;
							float speed = 10f;
							shockAmount++;
							Projectile.NewProjectile(source, position, toMouseNormalized * speed, ModContent.ProjectileType<CosmicClickerPro6>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
						}
					}
				}
				else
				{
					Projectile.timeLeft--;
				}
			}
			
			Projectile.alpha += Projectile.timeLeft < 40 ? 10 : 0;
			
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 4)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
				if (Projectile.frame >= 4)
				{
					Projectile.frame = 0;
				}
			}
		}
	}
}