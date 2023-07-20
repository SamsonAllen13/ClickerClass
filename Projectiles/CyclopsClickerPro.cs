using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class CyclopsClickerPro : ClickerProjectile
	{
		public const int Lifetime = 120;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.extraUpdates = 1;
			Projectile.timeLeft = Lifetime; //Technically not necessary, kills manually

			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 15;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255) * Projectile.Opacity;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			SpriteEffects spriteEffects = Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
			Rectangle origSourceRect = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);
			Vector2 origin = origSourceRect.Size() / 2f;

			//Extra offset
			origin.X = (Projectile.spriteDirection == 1) ? (origSourceRect.Width - 20) : 20;

			//TODO Generic projectile trail code, maybe make a method?
			int trailMax = 5;
			int trailStep = 1;
			int trailStart = 1;
			float trailMaxScale = 2.6f;
			float trailScaleDenominator = 15f;
			float trailRotAffection = 0f;
			Rectangle sourceRect = origSourceRect;

			Vector2 gfxOffY = new Vector2(0f, Projectile.gfxOffY);
			//Draw trail
			for (int i = trailStart; (trailStep > 0 && i < trailMax) || (trailStep < 0 && i > trailMax); i += trailStep)
			{
				if (i >= Projectile.oldPos.Length)
				{
					continue;
				}

				float lastTrailIndex = trailMax - i;
				if (trailStep < 0)
				{
					lastTrailIndex = trailStart - i;
				}

				Color trailColor = Color.Black * Projectile.Opacity;
				trailColor *= lastTrailIndex / (ProjectileID.Sets.TrailCacheLength[Projectile.type] * 1.5f);
				Vector2 oldPos = Projectile.oldPos[i];
				float trailRot = Projectile.rotation;
				SpriteEffects trailEffects = spriteEffects;
				int mode = ProjectileID.Sets.TrailingMode[Projectile.type];
				if (mode == 2 || mode == 3 || mode == 4)
				{
					trailRot = Projectile.oldRot[i];
					trailEffects = (Projectile.oldSpriteDirection[i] == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
				}

				if (oldPos == Vector2.Zero)
				{
					continue;
				}

				Vector2 trailPos = oldPos + Projectile.Size / 2f - Main.screenPosition + gfxOffY;
				Main.EntitySpriteDraw(texture, trailPos, sourceRect, trailColor, trailRot + Projectile.rotation * trailRotAffection * (i - 1f) * -spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(), origin, MathHelper.Lerp(Projectile.scale, trailMaxScale, i / trailScaleDenominator), trailEffects, 0);
			}

			Color outlineColor = Color.Lerp(Color.Red, Color.White, 0.5f);

			//Draw "spooky" outline
			outlineColor = outlineColor * 0.5f * Projectile.Opacity;
			float outlineRotationDenominator = 60;
			for (int i = 0; i < 4; i++)
			{
				Vector2 outlinePos = Projectile.Center - Main.screenPosition + gfxOffY + Projectile.rotation.ToRotationVector2().RotatedBy((180 + Counter) / outlineRotationDenominator * MathHelper.TwoPi + MathHelper.PiOver2 * i) * 6f;
				Main.EntitySpriteDraw(texture, outlinePos, origSourceRect, outlineColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			}

			//Draw actual projectile
			Color drawColor = Color.Black * Projectile.Opacity;
			float drawScale = Projectile.scale;
			float drawRotation = Projectile.rotation;
			Vector2 drawPos = Projectile.Center - Main.screenPosition + gfxOffY;
			Main.EntitySpriteDraw(texture, drawPos, origSourceRect, drawColor, drawRotation, origin, drawScale, spriteEffects, 0);

			return false;
		}

		public ref float Counter => ref Projectile.ai[0];
		public ref float SoundSlot => ref Projectile.localAI[1];

		public override void AI()
		{
			float progress = Counter / Lifetime;
			if (Counter == 0f)
			{
				SoundSlot = SoundEngine.PlaySound(SoundID.DD2_GhastlyGlaiveImpactGhost, Projectile.Center).ToFloat();
			}

			if (!SoundEngine.TryGetActiveSound(SlotId.FromFloat(SoundSlot), out var sound))
			{
				SoundSlot = SlotId.Invalid.ToFloat();
			}
			else
			{
				sound.Position = Projectile.Center;
			}

			float fadeOutTime = Lifetime - 15f;
			if (Counter > fadeOutTime)
			{
				Projectile.alpha += 10;
				if (Projectile.alpha > 255)
				{
					Projectile.alpha = 255;
				}
			}
			else
			{
				Projectile.alpha -= 25;
				if (Projectile.alpha < 50)
				{
					Projectile.alpha = 50;
				}
			}

			if (Counter >= Lifetime - 1f)
			{
				Projectile.Kill();
				return;
			}

			float offset = 70f;
			Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0f).ToDirectionInt();
			if (Projectile.velocity.Length() > 0.1f)
			{
				Projectile.velocity *= 0.95f;
			}

			offset *= Projectile.direction;
			Vector2 value = Projectile.Center - Projectile.rotation.ToRotationVector2() * offset;
			float windUp = Utils.Remap(progress, 0.3f, 0.5f, 0f, 1f) * Utils.Remap(progress, 0.45f, 0.5f, 1f, 0f);
			float swing = Utils.Remap(progress, 0.5f, 0.55f, 0f, 1f) * Utils.Remap(progress, 0.5f, 0.95f, 1f, 0.05f);
			float bonusRot = windUp * MathHelper.Pi / 60f;
			bonusRot += swing * -MathHelper.Pi * 5.5f / 60f;
			Projectile.rotation += bonusRot * -Projectile.direction;
			Projectile.rotation = MathHelper.WrapAngle(Projectile.rotation);
			Projectile.Center = value + Projectile.rotation.ToRotationVector2() * offset;

			Counter += 1f;
		}
	}
}
