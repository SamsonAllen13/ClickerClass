using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	//Currently unused, replaced by vanilla projectile
	public class RainbowClickerPro : ClickerProjectile
	{
		public bool HasSpawnEffects
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 25;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;

			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Celestial);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.aiStyle = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 2;
			AIType = ProjectileID.Bullet;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 180;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.Lerp(Color.White, Main.DiscoColor, 0.75f) * 1f;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
			Rectangle frame = texture2D.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);
			SpriteEffects effects = Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			Vector2 drawOrigin = new Vector2(texture2D.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Main.EntitySpriteDraw(texture2D, drawPos, frame, Color.Lerp(Color.White, Main.DiscoColor, 0.65f) * 0.05f, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
			}
			return true;
		}

		//TODO - Figure out how to replicate Empress of Light 'Rainbow Attacks' draw effects

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;
				SoundEngine.PlaySound(SoundID.Item43, Projectile.Center);
				for (int l = 0; l < 7; l++)
				{
					int dustType = 86 + l;
					for (int k = 0; k < 5; k++)
					{
						if (dustType == 91)
						{
							Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 92, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 0, default, 1.5f);
							dust.shader = GameShaders.Armor.GetSecondaryShader(70, Main.LocalPlayer);
							dust.noGravity = true;
						}
						else
						{
							Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, dustType, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 0, default, 1.5f);
							dust.noGravity = true;
						}
					}
				}
			}

			if (Projectile.timeLeft < 580)
			{
				Projectile.friendly = true;

				float x = Projectile.Center.X;
				float y = Projectile.Center.Y;
				float dist = 1000f;
				bool found = false;

				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy() && Projectile.DistanceSQ(npc.Center) < dist * dist && Collision.CanHit(Projectile.Center, 1, 1, npc.Center, 1, 1))
					{
						float foundX = npc.Center.X;
						float foundY = npc.Center.Y;
						float abs = Math.Abs(Projectile.Center.X - foundX) + Math.Abs(Projectile.Center.Y - foundY);
						if (abs < dist)
						{
							dist = abs;
							x = foundX;
							y = foundY;
							found = true;
						}
					}
				}

				if (found)
				{
					float mag = 5f;
					Vector2 center = Projectile.Center;
					float toX = x - center.X;
					float toY = y - center.Y;
					float len = (float)Math.Sqrt((double)(toX * toX + toY * toY));
					len = mag / len;
					toX *= len;
					toY *= len;

					Projectile.velocity.X = (Projectile.velocity.X * 20f + toX) / 21f;
					Projectile.velocity.Y = (Projectile.velocity.Y * 20f + toY) / 21f;
				}
			}
		}
	}
}
