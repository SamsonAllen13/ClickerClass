using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.GameContent;

namespace ClickerClass.Projectiles
{
	public class ShadowyClickerPro : ClickerProjectile
	{
		public bool Spawned
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 4;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 180;
			AIType = ProjectileID.Bullet;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 180;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 150) * 1f;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
			Rectangle frame = texture2D.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);
			SpriteEffects effects = Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			Vector2 drawOrigin = new Vector2(texture2D.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Main.EntitySpriteDraw(texture2D, drawPos, frame, new Color(255, 255, 255, 0) * 0.1f, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
			}
			return true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.ShadowFlame, 300, false);
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;

				SoundEngine.PlaySound(2, (int)Projectile.Center.X, (int)Projectile.Center.Y, 104);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 27, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 255, default, 1f);
					dust.noGravity = true;
				}
			}

			Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;

			if (Main.rand.Next(3) == 0)
			{
				int DustID = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 27, 0f, 0f, 255, default(Color), 1f);
				Main.dust[DustID].noGravity = true;
				int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 27, 0f, 0f, 150, default(Color), 0.75f);
				Main.dust[DustID2].noGravity = true;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			for (int u = 0; u < 10; u++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 27, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), 255, default(Color), 1.25f);
				Main.dust[dust].noGravity = true;
			}

			if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = -oldVelocity.X;
			}
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = -oldVelocity.Y;
			}
			return false;
		}

		public override void Kill(int timeLeft)
		{
			for (int u = 0; u < 15; u++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 27, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 255, default(Color), 1.25f);
				Main.dust[dust].noGravity = true;
			}
		}

		public override void PostAI()
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 4)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= 4)
			{
				Projectile.frame = 0;
				return;
			}
		}
	}
}
