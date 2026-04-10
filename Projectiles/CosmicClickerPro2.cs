using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class CosmicClickerPro2 : ClickerProjectile
	{
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

			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Ice, projsInheritProjElements: true);
			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Wind, projsInheritProjElements: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 62;
			Projectile.height = 62;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
		}
		
		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			
			Main.EntitySpriteDraw(texture2D, Projectile.Center - Main.screenPosition, null, (new Color(255, 255, 255, 150) * 0.25f) * Projectile.Opacity, -Projectile.rotation / 2, drawOrigin, Projectile.scale + 0.3f, SpriteEffects.None, 0);
			return true;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return (new Color(255, 255, 255, 150) * 0.8f) * Projectile.Opacity;
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;

				SoundEngine.PlaySound(SoundID.Item46.WithPitchOffset(0.25f), Projectile.Center);
				for (int k = 0; k < 20; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 92, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 0, default, 2f);
					dust.noGravity = true;
				}
			}
			
			Projectile.rotation += 0.05f;

			Timer++;
			if (Timer > 6 && Projectile.alpha == 0)
			{
				if (Main.myPlayer == Projectile.owner)
				{
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f)), ModContent.ProjectileType<CosmicClickerPro3>(), (int)(Projectile.damage * 0.1f), Projectile.knockBack, Projectile.owner);
				}
				Timer = 0;
			}
			
			Projectile.alpha += Projectile.timeLeft < 20 ? 20 : 0;

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

			if (Projectile.timeLeft < 30)
			{
				Projectile.alpha += 8;
			}
		}
	}
}
