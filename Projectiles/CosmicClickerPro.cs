using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class CosmicClickerPro : ClickerProjectile
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
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;

			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Fire);
			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Shadow);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 52;
			Projectile.height = 52;
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
				if (k % 3 == 0)
				{
					Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
					Main.EntitySpriteDraw(texture2D, drawPos, frame, new Color(255, 255, 255, 0) * 0.15f, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
				}
			}
			return true;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.OnFire3, 120, false);
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;

				SoundEngine.PlaySound(SoundID.Item74, Projectile.Center);
				for (int k = 0; k < 10; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 174, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 0, default, 1f);
					dust.noGravity = true;
				}
			}

			Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;

			if (Main.rand.Next(3) == 0)
			{
				int DustID = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 0, default(Color), 1.5f);
				Main.dust[DustID].noGravity = true;
				int DustID2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 174, 0f, 0f, 0, default(Color), 1f);
				Main.dust[DustID2].noGravity = true;
			}
			
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

		public override void OnKill(int timeLeft)
		{
			for (int u = 0; u < 15; u++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 174, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 0, default(Color), 1f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}
