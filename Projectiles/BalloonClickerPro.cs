using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ReLogic.Content;
using System.IO;
using Terraria.Audio;

namespace ClickerClass.Projectiles
{
	public class BalloonClickerPro : ClickerProjectile
	{
		public static Asset<Texture2D> effect;

		public override void Load()
		{
			effect = Mod.Assets.Request<Texture2D>("Projectiles/BalloonClickerPro_Effect");
		}

		public override void Unload()
		{
			effect = null;
		}
		
		public bool hasChanged = false;

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write((bool)hasChanged);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			hasChanged = reader.ReadBoolean();
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 6;
			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 26;
			Projectile.height = 26;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 0;
			Projectile.tileCollide = false;

			DrawOriginOffsetY = -2;
			DrawOffsetX = Projectile.width / 2 - 44 / 2;

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		private const int BalloonOffset = 22;

		public override bool PreDraw(ref Color lightColor)
		{
			if (!hasChanged)
			{
				Texture2D effectTexture = effect.Value;
				Rectangle frame = effectTexture.Frame();
				Vector2 drawOrigin = new Vector2(effectTexture.Width * 0.5f, Projectile.height * 0.5f - DrawOriginOffsetY + BalloonOffset);
				Main.EntitySpriteDraw(effectTexture, new Vector2(Projectile.Center.X /*+ DrawOffsetX*/ - 1, Projectile.Center.Y /*- BalloonOffset*/) - Main.screenPosition, frame, lightColor, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			if (Main.myPlayer == Projectile.owner && !hasChanged && Projectile.ai[0] == 1f)
			{
				hasChanged = true;
				Projectile.ai[0] = 0f;
				Projectile.timeLeft = 300;
				Projectile.netUpdate = true;
			}
			
			if (hasChanged)
			{
				if (Projectile.aiStyle != 26)
				{
					for (int k = 0; k < 8; k++)
					{
						Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(6, 6 + BalloonOffset), 12, 12, 115, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 100, default, 1.25f);
						dust.noGravity = true;
					}
					SoundEngine.PlaySound(4, (int)Projectile.Center.X, (int)Projectile.Center.Y, 63);
				}

				Projectile.aiStyle = 26;
				Projectile.velocity.Y += 0.1f;
				AIType = ProjectileID.BabySlime;
			}
			else
			{
				if (Projectile.timeLeft > 510)
				{
					Projectile.velocity.Y *= 0.96f;
				}
				else
				{
					int y = (int)Projectile.position.Y / 16;
					if (y < Main.worldSurface && y > 150)
					{
						//Only affected by wind if on the surface
						Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X + Main.windSpeedTarget * 0.15f, -2f, 2f);
					}
					Projectile.rotation = Projectile.velocity.X * 0.04f;
				}
			}
			
			if (Projectile.timeLeft < 30)
			{
				Projectile.alpha += 8;
			}
		}

		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			hitbox.Inflate(2, 2);
		}

		public override bool MinionContactDamage()
		{
			return hasChanged;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			//Always fall through. For conditional fallthrough, would require writing own AI (currently just clones vanilla)
			fallThrough = true;
			return true;
		}

		public override void Kill(int timeLeft)
		{
			for (int u = 0; u < 10; u++)
			{
				int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y - 20), 20, 20, 33, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 100, default(Color), 1.5f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}
