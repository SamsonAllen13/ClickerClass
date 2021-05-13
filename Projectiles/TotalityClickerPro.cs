using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class TotalityClickerPro : ClickerProjectile
	{
		public bool shift = false;
		public float pulse = 0f;
		public float rotation = 0f;
		
		public bool Spawned
		{
			get => projectile.ai[0] == 1f;
			set => projectile.ai[0] = value ? 1f : 0f;
		}

		public int Timer
		{
			get => (int)projectile.ai[1];
			set => projectile.ai[1] = value;
		}

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
			Texture2D texture = mod.GetTexture("Projectiles/TotalityClickerPro_Effect");
			spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, new Color(255, 255, 255, 0) * ((0.65f + pulse) * (0.01f * projectile.timeLeft)), rotation, new Vector2(58, 58), 1.35f + pulse, SpriteEffects.None, 0f);
			return true;
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;
				Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 43);
			}

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

			Timer++;
			if (Timer > 20)
			{
				int index = -1;
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy() && projectile.DistanceSQ(npc.Center) < 400f * 400f && Collision.CanHit(projectile.Center, 1, 1, npc.Center, 1, 1))
					{
						index = i;
					}
				}
				if (index != -1)
				{
					Vector2 vector = Main.npc[index].Center - projectile.Center;
					float speed = 3f;
					float mag = vector.Length();
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
				Timer = 0;
			}
		}
	}
}
