using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.GameContent;

namespace ClickerClass.Projectiles
{
	public class SpiralClickerPro : ClickerProjectile
	{
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

		public int RadiusDecrease
		{
			get => (int)Projectile.localAI[0];
			set => Projectile.localAI[0] = value;
		}

		public float Rot
		{
			get => Projectile.localAI[1];
			set => Projectile.localAI[1] = value;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 600;
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
				Main.EntitySpriteDraw(texture2D, drawPos, null, color * (0.0025f * Projectile.timeLeft), Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}

		public override void AI()
		{
			/*
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;

				Main.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 22);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 91, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1.25f);
					dust.noGravity = true;
				}
			}
			*/

			Player player = Main.player[Projectile.owner];
			
			Projectile.rotation += 0.2f;
			RadiusDecrease += 1;
			Rot += 0.05f;
			float clickerRadiusReal = player.GetModPlayer<ClickerPlayer>().ClickerRadiusReal;
			Projectile.Center = player.Center + new Vector2(0, clickerRadiusReal - RadiusDecrease).RotatedBy(Rot + (Index * (MathHelper.TwoPi / 2)));
			Projectile.gfxOffY = player.gfxOffY;
			if (RadiusDecrease >= clickerRadiusReal)
			{
				Projectile.Kill();
			}
		}
	}
}
