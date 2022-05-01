using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using ReLogic.Content;

namespace ClickerClass.Projectiles
{
	public class SandstormClickerPro : ClickerProjectile
	{
		public static Asset<Texture2D> effect;

		public override void Load()
		{
			effect = Mod.Assets.Request<Texture2D>("Projectiles/SandstormClickerPro_Effect");
		}

		public override void Unload()
		{
			effect = null;
		}

		private readonly HashSet<int> hitTargets = new HashSet<int>();
		private readonly HashSet<int> foundTargets = new HashSet<int>();
		public float alpha = 0.85f;
		public bool fadeOut = false;
		public bool homing = false;

		public bool Spawned
		{
			get => Projectile.localAI[0] == 1f;
			set => Projectile.localAI[0] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			DisplayName.SetDefault("Sandstorm Clicker");

			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		
		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 60;
			Projectile.height = 60;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 2;
			Projectile.ignoreWater = true;

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override void PostDraw(Color lightColor)
		{
			SpriteEffects effects = Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Texture2D texture2D = effect.Value;
			Vector2 drawOrigin = new Vector2(texture2D.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				float scaleDown = 0.067f * (15 - k);
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Main.EntitySpriteDraw(texture2D, drawPos, null, new Color(255, 255, 255, 0) * (alpha * 0.25f), Projectile.rotation, drawOrigin, scaleDown, effects, 0);
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 100) * alpha;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			width = Projectile.width / 2;
			height = Projectile.height / 2;
			return true;
		}

		public override bool? CanHitNPC(NPC target)
		{
			if (hitTargets.Contains(target.whoAmI))
			{
				return false;
			}
			return base.CanHitNPC(target);
		}

		public override void AI()
		{
			bool killed = Projectile.HandleChaining(hitTargets, foundTargets, 10);
			if (killed)
			{
				return;
			}

			if (!Spawned)
			{
				Spawned = true;

				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 8);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 216, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 150, default, 1.25f);
					dust.noGravity = true;
				}
			}

			int dir = (Projectile.velocity.X > 0f).ToDirectionInt();
			Projectile.rotation += dir * 0.08f;
			Projectile.spriteDirection = dir;

			float x = Projectile.Center.X;
			float y = Projectile.Center.Y;
			float dist = 800;
			bool found = false;

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.CanBeChasedBy() && !hitTargets.Contains(npc.whoAmI) && Projectile.DistanceSQ(npc.Center) < dist * dist && Collision.CanHit(Projectile.Center, 1, 1, npc.Center, 1, 1))
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
				float mag = 4f;
				Vector2 center = Projectile.Center;
				float toX = x - center.X;
				float toY = y - center.Y;
				float len = (float)Math.Sqrt(toX * toX + toY * toY);
				len = mag / len;
				toX *= len;
				toY *= len;

				Projectile.velocity.X = (Projectile.velocity.X * 20f + toX) / 21f;
				Projectile.velocity.Y = (Projectile.velocity.Y * 20f + toY) / 21f;
				homing = true;
			}
			else
			{
				homing = false;
			}
			
			if (Projectile.timeLeft < 30)
			{
				fadeOut = true;
			}
			
			if (fadeOut)
			{
				alpha -= 0.05f;
				if (alpha < 0f)
				{
					Projectile.Kill();
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (!homing && !fadeOut)
			{
				fadeOut = true;
				Projectile.friendly = false;
				Projectile.tileCollide = false;
				Projectile.timeLeft = 30;
				Projectile.velocity = oldVelocity;
			}
			return false;
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 10; k++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 216, Main.rand.Next((int)-5f, (int)5f), Main.rand.Next((int)-5f, (int)5f), 200, default(Color), 1.25f);
				Main.dust[dust].noGravity = true;

				dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 216, Main.rand.Next((int)-3f, (int)3f), Main.rand.Next((int)-3f, (int)3f), 50, default(Color), 1f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}
