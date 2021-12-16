using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.GameContent;
using ReLogic.Content;

namespace ClickerClass.Projectiles
{
	public class CaptainsClickerPro : ClickerProjectile
	{
		public Vector2 Location => new Vector2(Projectile.ai[0], Projectile.ai[1]);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 300;
			Projectile.extraUpdates = 1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 300, false);
		}

		public override bool PreDraw(ref Color lightColor)
		{
			if (Projectile.timeLeft > 4)
			{
				Asset<Texture2D> asset = TextureAssets.Projectile[Projectile.type];
				Vector2 drawOrigin = new Vector2(asset.Width() * 0.5f, Projectile.height * 0.5f);
				for (int k = 0; k < Projectile.oldPos.Length; k++)
				{
					Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
					Color color = Projectile.GetAlpha(lightColor * 0.25f) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
					Main.EntitySpriteDraw(asset.Value, drawPos, null, color * 0.25f, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
				}
			}
			return true;
		}

		public override void AI()
		{
			Projectile.rotation += Projectile.velocity.X > 0f ? 0.35f : -0.35f;

			for (int l = 0; l < 2; l++)
			{
				int num235 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y) - Projectile.velocity, Projectile.width, Projectile.height, 31, 0f, 0f, 125, default(Color), 1f);
				Dust dust4 = Main.dust[num235];
				dust4.velocity *= 0f;
				Main.dust[num235].noGravity = true;
			}

			Vector2 vec = Location;
			if (Projectile.DistanceSQ(vec) <= 10 * 10)
			{
				if (Projectile.timeLeft > 4)
				{
					SoundEngine.PlaySound(2, (int)Projectile.Center.X, (int)Projectile.Center.Y, 89);
					Projectile.timeLeft = 4;

					for (int k = 0; k < 10; k++)
					{
						Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 6, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 0, default, 1.35f);
						dust.noGravity = true;
					}
					for (int k = 0; k < 20; k++)
					{
						Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 174, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 0, default, 1.35f);
						dust.noGravity = true;
					}
					for (int k = 0; k < 15; k++)
					{
						Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 31, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 125, default, 1f);
						dust.noGravity = true;
					}
				}
			}

			if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
			{
				Projectile.velocity.X = 0f;
				Projectile.velocity.Y = 0f;
				Projectile.tileCollide = false;
				Projectile.friendly = true;
				Projectile.alpha = 255;
				Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
				Projectile.width = 200;
				Projectile.height = 200;
				Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			}
		}
	}
}