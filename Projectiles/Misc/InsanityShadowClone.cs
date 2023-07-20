using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles.Misc
{
	//Demonstration purposes only!
	/// <summary>
	/// Fully functional clone of InsanityShadowHostile (965) except for its shadow color and the SetDefaults being mostly from the friendly variant (964)
	/// <br/>Contains a static method to get its proper "configuration" for NewProjectile
	/// </summary>
	public abstract class InsanityShadowClone : ModProjectile //Made abstract so it isn't loaded as we don't need it
	{
		/// <summary>
		/// InsanityShadowClone.RandomizeInsanityShadowFor(target, isHostile, out Vector2 spawnposition, out Vector2 spawnvelocity, out float ai0, out float ai1);
		/// Projectile.NewProjectile(source, spawnposition, spawnvelocity, type, damage, knockback, whoAmI, ai0, ai1);
		/// </summary>
		public static void RandomizeInsanityShadowFor(Entity targetEntity, bool isHostile, out Vector2 spawnposition, out Vector2 spawnvelocity, out float ai0, out float ai1)
		{
			int veloLength = Main.rand.Next(2) * 2 - 1;
			int variation = Main.rand.Next(4);
			//See GetVariationValues comments for what a variation represents
			float dist = isHostile ? 200f : 100f;
			float distDenom = isHostile ? 30 : 20;
			float scaleFactor = isHostile ? 30 : 0;
			float startRot = Main.rand.NextFloatDirection() * MathHelper.Pi / 8;
			float randomCircle = Main.rand.NextFloat(MathHelper.TwoPi);
			Vector2 randomCircleVector = randomCircle.ToRotationVector2();
			if (isHostile && targetEntity.velocity.X * veloLength > 0f)
			{
				veloLength *= -1;
			}

			if (variation == 0 && isHostile)
			{
				distDenom += 10f;
			}

			spawnposition = targetEntity.Center + targetEntity.velocity * scaleFactor + new Vector2(veloLength * -dist, 0f).RotatedBy(startRot);
			spawnvelocity = new Vector2(veloLength * dist / distDenom, 0f).RotatedBy(startRot);
			//Converts variation into a pair of ai0/ai1 values to deconstruct in AI later
			ai0 = 0f;
			ai1 = 0f;
			if (variation == 1)
			{
				spawnposition = targetEntity.Center - randomCircleVector * (isHostile ? dist : (dist * 0.5f));
				ai0 = 180f;
				ai1 = randomCircle - MathHelper.PiOver2;
				spawnvelocity = randomCircleVector * (isHostile ? 4 : 2);
			}
			else if (variation == 2)
			{
				spawnposition = targetEntity.Center - randomCircleVector * dist;
				ai0 = 300f;
				ai1 = randomCircle;
				spawnvelocity = randomCircleVector * (isHostile ? 4 : 2);
			}
			else if (variation == 3)
			{
				float spawnDist = isHostile ? 60 : 30;
				float randomRot = MathHelper.PiOver2 / spawnDist * Main.rand.NextFloatDirection();
				spawnposition = targetEntity.Center + targetEntity.velocity * spawnDist;
				Vector2 vector = randomCircleVector * (isHostile ? 8 : 3);
				for (int i = 0; i < spawnDist; i++)
				{
					spawnposition -= vector;
					vector = vector.RotatedBy(-randomRot);
				}

				spawnvelocity = vector;
				ai0 = 390f;
				ai1 = randomRot;
			}
		}

		/// <summary>
		/// Changes how it draws (purple or red)
		/// </summary>
		public bool Hostile => true;
		public ref float SoundSlot => ref Projectile.localAI[1];

		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.InsanityShadowFriendly;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			//Hostile:
			//Projectile.width = 40;
			//Projectile.height = 40;
			//Projectile.aiStyle = 187;
			//Projectile.hostile = true;
			//Projectile.tileCollide = false;
			//Projectile.ignoreWater = true;
			//Projectile.timeLeft = 300;
			//Projectile.alpha = 255;

			//Friendly:
			Projectile.width = 40;
			Projectile.height = 40;
			//Projectile.aiStyle = 187;
			Projectile.alpha = 255;
			Projectile.penetrate = 3;
			Projectile.friendly = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1; //Only damages an enemy once, damages penetrate amount of enemies before disappearing
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.extraUpdates = 1;
			Projectile.scale = 0.7f;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255) * Projectile.Opacity;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			//Decreases damage after each hit
			Projectile.damage = (int)(Projectile.damage * 0.85f);
			if (Projectile.damage < 1) Projectile.damage = 1;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			SpriteEffects spriteEffects = Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
			Rectangle origSourceRect = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);
			Vector2 origin = origSourceRect.Size() / 2f;

			//Extra offset
			origin.X = (Projectile.spriteDirection == 1) ? (origSourceRect.Width - 20) : 20;

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

			Color outlineColor = Color.Violet;
			if (Hostile)
			{
				outlineColor = Color.Lerp(Color.Red, Color.White, 0.5f);
			}

			//Draw "spooky" outline
			outlineColor = outlineColor * 0.5f * Projectile.Opacity;
			float outlineRotationDenominator = !Hostile ? 60 : 30;
			for (int i = 0; i < 4; i++)
			{
				Vector2 outlinePos = Projectile.Center - Main.screenPosition + gfxOffY + Projectile.rotation.ToRotationVector2().RotatedBy(Projectile.ai[0] / outlineRotationDenominator * MathHelper.TwoPi + MathHelper.PiOver2 * i) * 6f;
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

		public void GetVariationValues(out int variation, out float fakeCounter, out float counterMax)
		{
			//Variation doc:
			//0: slow dash
			//1: in-place swipe
			//2: fast dash
			//3: half-circle swipe
			fakeCounter = Projectile.ai[0];
			variation = 0;
			float prevCounter = 0f;
			counterMax = 180f;
			float counterMaxDummy = counterMax;
			if (fakeCounter >= prevCounter && fakeCounter < counterMaxDummy)
			{
				variation = 0;
				counterMax = counterMaxDummy;
				return;
			}

			prevCounter = counterMaxDummy;
			counterMaxDummy += 120f;
			if (fakeCounter >= prevCounter && fakeCounter < counterMaxDummy)
			{
				variation = 1;
				fakeCounter -= prevCounter;
				counterMax = counterMaxDummy - prevCounter;
				return;
			}

			prevCounter = counterMaxDummy;
			counterMaxDummy += 90f;
			if (fakeCounter >= prevCounter && fakeCounter < counterMaxDummy)
			{
				variation = 2;
				fakeCounter -= prevCounter;
				counterMax = counterMaxDummy - prevCounter;
			}

			prevCounter = counterMaxDummy;
			counterMaxDummy += 90f;
			if (fakeCounter >= prevCounter && fakeCounter < counterMaxDummy)
			{
				variation = 3;
				fakeCounter -= prevCounter;
				counterMax = counterMaxDummy - prevCounter;
			}
		}

		public override void AI()
		{
			GetVariationValues(out int variation, out float fakeCounter, out float counterMax);

			float fromValue = fakeCounter / counterMax;
			if (fakeCounter == 0f)
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

			float fadeOutTime = counterMax - 15f;
			if (fakeCounter > fadeOutTime)
			{
				Projectile.alpha += 25;
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

			if (fakeCounter >= counterMax - 1f)
			{
				Projectile.Kill();
				return;
			}

			if (variation == 0)
			{
				Projectile.velocity *= 0.98f;
				Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0f).ToDirectionInt();
				Projectile.rotation = Projectile.velocity.ToRotation();
				if (Projectile.spriteDirection == -1)
				{
					Projectile.rotation += MathHelper.Pi;
				}
			}
			else if (variation == 1)
			{
				float offset = 70f;
				Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0f).ToDirectionInt();
				if (Projectile.velocity.Length() > 0.1f)
				{
					Projectile.velocity *= 0.95f;
				}

				offset *= Projectile.direction;
				Vector2 value = Projectile.Center - Projectile.rotation.ToRotationVector2() * offset;
				float from03to05 = Utils.Remap(fromValue, 0.3f, 0.5f, 0f, 1f) * Utils.Remap(fromValue, 0.45f, 0.5f, 1f, 0f); //Wind-up
				float from05to1 = Utils.Remap(fromValue, 0.5f, 0.55f, 0f, 1f) * Utils.Remap(fromValue, 0.5f, 1f, 1f, 0f); //Swing
				float bonusRot = from03to05 * MathHelper.Pi / 60f;
				bonusRot += from05to1 * -MathHelper.Pi * 8f / 60f;
				Projectile.rotation += bonusRot * -Projectile.direction;
				Projectile.rotation = MathHelper.WrapAngle(Projectile.rotation);
				Projectile.Center = value + Projectile.rotation.ToRotationVector2() * offset;
			}
			else if (variation == 2)
			{
				float f = Projectile.ai[1];
				float from0to04 = Utils.Remap(fromValue, 0f, 0.4f, 1f, 0f);
				float from03to1 = Utils.Remap(fromValue, 0.3f, 0.4f, 0f, 1f) * Utils.Remap(fromValue, 0.4f, 1f, 1f, 0f);
				float velocityFactor = from0to04 * 2f + from03to1 * 8f + 0.01f;
				Projectile.velocity = f.ToRotationVector2() * velocityFactor;
				Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0f).ToDirectionInt();
				Projectile.rotation = Projectile.velocity.ToRotation();
				if (Projectile.spriteDirection == -1)
				{
					Projectile.rotation += MathHelper.Pi;
				}
			}
			else if (variation == 3)
			{
				float num8 = Projectile.ai[1];
				Projectile.velocity = Projectile.velocity.RotatedBy(num8);
				Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0f).ToDirectionInt();
				Projectile.rotation = Projectile.velocity.ToRotation();
				if (Projectile.spriteDirection == -1)
				{
					Projectile.rotation += MathHelper.Pi;
				}
			}

			Projectile.ai[0] += 1f;
		}
	}
}
