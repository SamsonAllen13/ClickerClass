using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class TotalityClickerPro : ClickerProjectile
	{
		public bool shift = false;
		public float pulse = 0f;
		public float rotation = 0f;

		public override void SetDefaults()
		{
			projectile.width = 50;
			projectile.height = 50;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.timeLeft = 240;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255) * (0.1f + (0.005f * projectile.timeLeft));
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			spriteBatch.Draw(mod.GetTexture("Projectiles/TotalityClickerPro_Effect"), projectile.Center - Main.screenPosition, null, new Color(255, 255, 255, 0) * ((0.65f + pulse) * (0.01f * projectile.timeLeft)), rotation, new Vector2(58, 58), 1.35f + pulse, SpriteEffects.None, 0f);
			return true;
		}

		public override void AI()
		{
			rotation += 0.01f;
			pulse += !shift ? 0.0035f : -0.0035f;
			if (pulse > 0.15f && !shift)
			{
				shift = true;
			}
			if (pulse <= 0f)
			{
				shift = false;
			}

			projectile.ai[0]++;
			if (projectile.ai[0] > 20)
			{
				int index = -1;
				for (int i = 0; i < 200; i++)
				{
					if (Vector2.Distance(projectile.Center, Main.npc[i].Center) < 400f && Main.npc[i].active && Collision.CanHit(projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
					{
						index = i;
					}
				}
				if (index != -1)
				{
					Vector2 vector = Main.npc[index].Center - projectile.Center;
					float speed = 3f;
					float mag = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
					if (mag > speed)
					{
						mag = speed / mag;
					}
					vector *= mag;

					if (projectile.owner == Main.myPlayer)
					{
						float numberProjectiles = 3;
						float rotation = MathHelper.ToRadians(20);
						for (int i = 0; i < numberProjectiles; i++)
						{
							Vector2 perturbedSpeed = new Vector2(vector.X, vector.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
							Projectile.NewProjectile(projectile.Center, perturbedSpeed, ModContent.ProjectileType<TotalityClickerPro2>(), (int)(projectile.damage * 0.25f), projectile.knockBack, projectile.owner);
						}
					}
				}
				projectile.ai[0] = 0;
			}
		}
	}
}