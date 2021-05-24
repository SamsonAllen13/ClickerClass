using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class SandstormClickerPro : ClickerProjectile
	{
		private readonly HashSet<int> hitTargets = new HashSet<int>();
		private readonly HashSet<int> foundTargets = new HashSet<int>();
		public float alpha = 0.85f;
		public bool fadeOut = false;
		public bool homing = false;

		public bool Spawned
		{
			get => projectile.localAI[0] == 1f;
			set => projectile.localAI[0] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		
		public override void SetDefaults()
		{
			projectile.width = 60;
			projectile.height = 60;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.alpha = 255;
			projectile.magic = true;
			projectile.friendly = true;
			projectile.timeLeft = 600;
			projectile.extraUpdates = 2;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			SpriteEffects effects = projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Texture2D texture2D = Main.projectileTexture[projectile.type];
			Vector2 drawOrigin = new Vector2(texture2D.Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				float scaleDown = 0.067f * (15 - k);
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				spriteBatch.Draw(mod.GetTexture("Projectiles/SandstormClickerPro_Effect"), drawPos, null, new Color(255, 255, 255, 0) * (alpha * 0.25f), projectile.rotation, drawOrigin, scaleDown, effects, 0f);
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 100) * alpha;
		}
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = projectile.width / 2;
			height = projectile.height / 2;
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
			bool killed = projectile.HandleChaining(hitTargets, foundTargets, 10);
			if (killed)
			{
				return;
			}

			if (!Spawned)
			{
				Spawned = true;

				Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 8);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(projectile.Center - new Vector2(4), 8, 8, 216, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 150, default, 1.25f);
					dust.noGravity = true;
				}
			}

			int dir = (projectile.velocity.X > 0f).ToDirectionInt();
			projectile.rotation += dir * 0.08f;
			projectile.spriteDirection = dir;

			float x = projectile.Center.X;
			float y = projectile.Center.Y;
			float dist = 800;
			bool found = false;

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.CanBeChasedBy() && !hitTargets.Contains(npc.whoAmI) && projectile.DistanceSQ(npc.Center) < dist * dist && Collision.CanHit(projectile.Center, 1, 1, npc.Center, 1, 1))
				{
					float foundX = npc.Center.X;
					float foundY = npc.Center.Y;
					float abs = Math.Abs(projectile.Center.X - foundX) + Math.Abs(projectile.Center.Y - foundY);
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
				Vector2 center = projectile.Center;
				float toX = x - center.X;
				float toY = y - center.Y;
				float len = (float)Math.Sqrt(toX * toX + toY * toY);
				len = mag / len;
				toX *= len;
				toY *= len;

				projectile.velocity.X = (projectile.velocity.X * 20f + toX) / 21f;
				projectile.velocity.Y = (projectile.velocity.Y * 20f + toY) / 21f;
				homing = true;
			}
			else
			{
				homing = false;
			}
			
			if (projectile.timeLeft < 30)
			{
				fadeOut = true;
			}
			
			if (fadeOut)
			{
				alpha -= 0.05f;
				if (alpha < 0f)
				{
					projectile.Kill();
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (!homing && !fadeOut)
			{
				fadeOut = true;
				projectile.friendly = false;
				projectile.tileCollide = false;
				projectile.timeLeft = 30;
				projectile.velocity = oldVelocity;
			}
			return false;
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 10; k++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 216, Main.rand.Next((int)-5f, (int)5f), Main.rand.Next((int)-5f, (int)5f), 200, default(Color), 1.25f);
				Main.dust[dust].noGravity = true;

				dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 216, Main.rand.Next((int)-3f, (int)3f), Main.rand.Next((int)-3f, (int)3f), 50, default(Color), 1f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}
