using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.GameContent;

namespace ClickerClass.Projectiles
{
	public class MyceliumClickerPro : ClickerProjectile
	{
		public bool HasSpawnEffects
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public int Timer
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 4;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		
		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 180;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}
		
		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
			Rectangle frame = texture2D.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);

			Vector2 drawOrigin = new Vector2(texture2D.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.spriteBatch.Draw(texture2D, drawPos, frame, color * 0.25f, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;

				SoundEngine.PlaySound(3, (int)Projectile.Center.X, (int)Projectile.Center.Y, 13);
				for (int k = 0; k < 20; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 41, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 200, default, 1.25f);
					dust.noGravity = true;
				}
			}

			if (Projectile.timeLeft < 170)
			{
				Projectile.friendly = true;
			}

			Timer++;
			if (Timer >= 0)
			{
				Projectile.velocity.Y += 1.015f;
				Timer = -15;
			}
		}
		
		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 8; k++)
			{
				int dust = Dust.NewDust(Projectile.position, (int)(Projectile.width * 0.5f), (int)(Projectile.height * 0.5f), 41, Main.rand.Next((int)-8f, (int)8f), Main.rand.Next((int)-8f, (int)8f), 200, default, 1.25f);
				Main.dust[dust].noGravity = true;
			}
		}
		
		public override void PostAI()
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 8)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= 4)
			{
				Projectile.frame = 0;
			}
		}
	}
}
