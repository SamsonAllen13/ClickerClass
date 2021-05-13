using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class TitaniumClickerPro : ClickerProjectile
	{
		public int radiusIncrease = 0;
		public float rot = 0f;
		public Vector2 center = Vector2.Zero;

		public int Index
		{
			get => (int)projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		public bool HasSpawnEffects
		{
			get => projectile.ai[1] == 1f;
			set => projectile.ai[1] = value ? 1f : 0f;
		}

		public int Timer
		{
			get => (int)projectile.localAI[0];
			set => projectile.localAI[0] = value;
		}

		public float Rotation
		{
			get => projectile.localAI[1];
			set => projectile.localAI[1] = value;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 30;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.timeLeft = 240;
			projectile.extraUpdates = 1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 20;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture2D = Main.projectileTexture[projectile.type];
			Vector2 drawOrigin = new Vector2(texture2D.Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor * 0.25f) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(texture2D, drawPos, null, color * (0.0025f * projectile.timeLeft), Rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;

				Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 22);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(projectile.Center - new Vector2(4), 8, 8, 91, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1.25f);
					dust.noGravity = true;
				}
			}

			if (center == Vector2.Zero)
			{
				center = projectile.Center;
			}
			projectile.rotation += projectile.velocity.X > 0f ? 0.2f : -0.2f;

			Timer++;
			if (Timer % 5 == 0)
			{
				Rotation = projectile.rotation;
			}

			radiusIncrease += 1;
			rot += 0.05f;
			projectile.Center = center + new Vector2(0, 20 + radiusIncrease).RotatedBy(rot + (Index * (MathHelper.TwoPi / 5)));
		}
	}
}
