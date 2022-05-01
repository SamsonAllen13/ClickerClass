using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.GameContent;

namespace ClickerClass.Projectiles
{
	public class TitaniumClickerPro : ClickerProjectile
	{
		public int radiusIncrease = 0;
		public float rot = 0f;
		public Vector2 center = Vector2.Zero;

		public int Index
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public bool HasSpawnEffects
		{
			get => Projectile.ai[1] == 1f;
			set => Projectile.ai[1] = value ? 1f : 0f;
		}

		public int Timer
		{
			get => (int)Projectile.localAI[0];
			set => Projectile.localAI[0] = value;
		}

		public float Rotation
		{
			get => Projectile.localAI[1];
			set => Projectile.localAI[1] = value;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 240;
			Projectile.extraUpdates = 1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
			Vector2 drawOrigin = new Vector2(texture2D.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor * 0.25f) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture2D, drawPos, null, color * (0.0025f * Projectile.timeLeft), Rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;

				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 22);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 91, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1.25f);
					dust.noGravity = true;
				}
			}

			if (center == Vector2.Zero)
			{
				center = Projectile.Center;
			}
			Projectile.rotation += Projectile.velocity.X > 0f ? 0.2f : -0.2f;

			Timer++;
			if (Timer % 5 == 0)
			{
				Rotation = Projectile.rotation;
			}

			radiusIncrease += 1;
			rot += 0.05f;
			Projectile.Center = center + new Vector2(0, 20 + radiusIncrease).RotatedBy(rot + (Index * (MathHelper.TwoPi / 5)));
		}
	}
}
