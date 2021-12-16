using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.GameContent;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.Graphics.Shaders;

namespace ClickerClass.Projectiles
{
	public class RGBPro : ClickerProjectile
	{
		public static Asset<Texture2D> effect;
		public static Asset<Texture2D> effect2;

		public override void Load()
		{
			effect = Mod.Assets.Request<Texture2D>("Projectiles/RGBPro_Effect");
			effect2 = Mod.Assets.Request<Texture2D>("Projectiles/RGBPro_Effect2");
		}

		public override void Unload()
		{
			effect = null;
			effect2 = null;
		}

		public int StateTimer
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public bool State_Waiting => StateTimer == 0;

		public bool State_Decide => StateTimer == 1;

		public bool HasSpawnEffects
		{
			get => Projectile.ai[1] == 1f;
			set => Projectile.ai[1] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 28;
			Projectile.height = 28;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.tileCollide = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 240;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 20;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White * Projectile.Opacity;
		}
		
		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture = effect.Value;
			Rectangle frame = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);
			Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, frame, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 75) * (0.8f * Projectile.Opacity), Projectile.rotation, new Vector2(14, 14), Projectile.scale, SpriteEffects.None, 0);
		
			if (!State_Waiting)
			{
				SpriteEffects effects = Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

				Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
				for (int k = 0; k < Projectile.oldPos.Length; k++)
				{
					Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
					Main.EntitySpriteDraw(texture, drawPos, frame, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 75) * (0.3f * Projectile.Opacity), Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
				}
			}
			return true;
		}
		
		public override void PostDraw(Color lightColor)
		{
			Texture2D texture = effect2.Value;
			Rectangle frame = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);
			Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, frame, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 50) * (0.25f * Projectile.Opacity), Projectile.rotation, new Vector2(14, 14), Projectile.scale, SpriteEffects.None, 0);
		}

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;

				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 109);
				for (int k = 0; k < 20; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 22, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 125, default, 1.5f);
					dust.noGravity = true;
					dust.noLight = true;
				}
			}
			
			if (State_Waiting)
			{
				Projectile.velocity *= 0.9f;
				if (Projectile.timeLeft <= 180)
				{
					StateTimer++;
				}
			}
			else if (State_Decide)
			{
				Projectile.extraUpdates = 1;
				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 86);
				
				for (int l = 0; l < 7; l++)
				{
					int dustType = 86 + l;
					for (int k = 0; k < 3; k++)
					{
						if (dustType == 91)
						{
							Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 92, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 0, default, 0.75f);
							dust.shader = GameShaders.Armor.GetSecondaryShader(70, Main.LocalPlayer);
							dust.noGravity = true;
						}
						else
						{
							Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, dustType, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 0, default, 0.75f);
							dust.noGravity = true;
						}
					}
				}
			
				if (Main.myPlayer == Projectile.owner)
				{
					Vector2 vector = Main.MouseWorld - Projectile.Center;
					float speed = 5f;
					float mag = vector.Length();
					if (mag > speed)
					{
						mag = speed / mag;
						vector *= mag;
					}
					Projectile.velocity = vector;
					Projectile.netUpdate = true;
				}
				
				StateTimer++;
				Projectile.frame = 1;
			}
			else
			{
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			}
			
			if (Projectile.timeLeft < 20)
			{
				Projectile.alpha += 8;
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int l = 0; l < 7; l++)
			{
				int dustType = 86 + l;
				for (int k = 0; k < 5; k++)
				{
					if (dustType == 91)
					{
						Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 92, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1f);
						dust.shader = GameShaders.Armor.GetSecondaryShader(70, Main.LocalPlayer);
						dust.noGravity = true;
					}
					else
					{
						Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, dustType, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1f);
						dust.noGravity = true;
					}
				}
			}
		}
	}
}
