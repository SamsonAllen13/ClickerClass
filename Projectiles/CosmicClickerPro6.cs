using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class CosmicClickerPro6 : ClickerProjectile
	{
		public float Timer
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public float AmpCount
		{
			get => Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 50;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.timeLeft = 300;
			Projectile.extraUpdates = 4;
			DrawOffsetX = -9;
			DrawOriginOffsetX = 4;

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 15;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 0) * 0f;
		}

		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			hitbox.Inflate(16, 16);
		}

		public override bool PreDraw(ref Color lightColor)
		{
			//Technically incorrect usage of drawOffsetX and similar, but needed so that the trail aligns properly with zigzag
			Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
			Vector2 drawOrigin = new Vector2((texture2D.Width - Projectile.width) / 2 + Projectile.width / 2 + DrawOriginOffsetX, Projectile.height / 2);
			//lightColor *= Math.Min(projectile.velocity.Length() / 3, 1);
			for (int k = Projectile.oldPos.Length - 1; k > 0; k--)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(DrawOffsetX, Projectile.gfxOffY);
				Color color = (new Color(255, 255, 255, 0) * 0.75f) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture2D, drawPos, null, color, Projectile.oldRot[k], drawOrigin, Projectile.scale + 0.1f, SpriteEffects.None, 0);
				Main.EntitySpriteDraw(ModContent.Request<Texture2D>(Texture + "_Effect").Value, drawPos, null, color, Projectile.oldRot[k], drawOrigin, Projectile.scale - 0.1f, SpriteEffects.None, 0);
			}
			return true;
		}

		public override void AI()
		{
			if (Projectile.timeLeft <= 60)
			{
				Projectile.tileCollide = false;
				Projectile.velocity = Vector2.Zero;
				return;
			}
			
			float addAmplitude = 0.04f;
			float cicleDiv = 24f;

			float waveStep = MathHelper.TwoPi / cicleDiv;
			float amplitude = 6 + AmpCount;

			float time = Timer * waveStep;
			if (Timer == 0 && Projectile.velocity.X > 0)
			{
				time += MathHelper.Pi;
			}

			float curHeight = HeightMult(time) * amplitude;

			float realSpeed;
			float realRot;

			if (Timer == 0)
			{
				realSpeed = Projectile.velocity.Length();
				realRot = Projectile.velocity.ToRotation();
				if (Projectile.velocity.X > 0)
				{
					Timer += cicleDiv / 2;
				}
			}
			else
			{
				float heightDiff = curHeight - HeightMult(time - waveStep) * (amplitude - addAmplitude);
				realSpeed = (float)Math.Sqrt(Projectile.velocity.LengthSquared() - heightDiff * heightDiff);
				realRot = Projectile.velocity.RotatedBy(-(new Vector2(realSpeed, heightDiff).ToRotation())).ToRotation();
			}

			Projectile.velocity = new Vector2(Math.Min(realSpeed * 1.025f, 5f), HeightMult(time + waveStep) * (amplitude + addAmplitude) - curHeight).RotatedBy(realRot);

			Timer++;
			AmpCount += addAmplitude;

			float HeightMult(float counter)
			{
				float sinCounter = (counter + MathHelper.PiOver2) % MathHelper.TwoPi / MathHelper.Pi;
				return (sinCounter < 1 ? sinCounter - 0.5f : 0.5f - (sinCounter - 1)) * 2;
			}
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.timeLeft = 60;
			return false;
		}

		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 10; k++)
			{
				Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 15, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 150, default, 1.25f).noGravity = true;
			}
		}
	}
}
