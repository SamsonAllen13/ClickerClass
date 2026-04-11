using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class CosmicClickerPro7 : ClickerProjectile
	{
		public bool Spawned
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;

			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Celestial);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 42;
			Projectile.height = 42;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 180;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 180;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return (new Color(255, 255, 255, 255) * 1f) * Projectile.Opacity;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				float scaleDown = (1f / ProjectileID.Sets.TrailCacheLength[Projectile.type]) * (ProjectileID.Sets.TrailCacheLength[Projectile.type] - k);
				float alphaDown = (.5f / ProjectileID.Sets.TrailCacheLength[Projectile.type]) * (ProjectileID.Sets.TrailCacheLength[Projectile.type] - k);
				lightColor = new Color(255, 255, 255, 255);
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Main.EntitySpriteDraw(texture2D, drawPos, null, ((lightColor * alphaDown) * 0.5f) * Projectile.Opacity, 0f, drawOrigin, scaleDown, SpriteEffects.None, 0);
			}
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			if (!Spawned)
			{
				Spawned = true;

				SoundEngine.PlaySound(SoundID.Item117, Projectile.Center);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 15, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 255, default, 2f);
					dust.noGravity = true;
				}
			}

			Projectile.rotation += Projectile.velocity.X > 0f ? 0.1f : -0.1f;
		}

		public override void OnKill(int timeLeft)
		{
			for (int u = 0; u < 20; u++)
			{
				int dust = Dust.NewDust(Projectile.Center, 10, 10, 15, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 200, default(Color), 2.5f);
				Main.dust[dust].noGravity = true;
				int dust2 = Dust.NewDust(Projectile.Center, 10, 10, 15, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 255, default(Color), 1f);
				Main.dust[dust2].noGravity = true;
			}
		}
	}
}
