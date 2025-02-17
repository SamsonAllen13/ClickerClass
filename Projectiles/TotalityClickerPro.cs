using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class TotalityClickerPro : ClickerProjectile
	{
		public static Lazy<Asset<Texture2D>> effect;

		public override void Load()
		{
			effect = new(() => ModContent.Request<Texture2D>(Texture + "_Effect"));
		}

		public override void Unload()
		{
			effect = null;
		}

		public bool shift = false;
		public float pulse = 0f;
		public float rotation = 0f;

		public bool Spawned
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public int Timer
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Fire, projsInheritProjElements: true);
			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Shadow, projsInheritProjElements: true);
			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Celestial, projsInheritProjElements: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 240;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255) * (0.1f + (0.005f * Projectile.timeLeft));
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture = effect.Value.Value;
			Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, new Color(255, 255, 255, 0) * ((0.65f + pulse) * (0.01f * Projectile.timeLeft)), rotation, new Vector2(58, 58), 1.35f + pulse, SpriteEffects.None, 0);
			return true;
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;
				SoundEngine.PlaySound(SoundID.Item43, Projectile.Center);
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
					if (npc.CanBeChasedBy() && Projectile.DistanceSQ(npc.Center) < 400f * 400f && Collision.CanHit(Projectile.Center, 1, 1, npc.Center, 1, 1))
					{
						index = i;
					}
				}
				if (index != -1)
				{
					Vector2 vector = Main.npc[index].Center - Projectile.Center;
					float speed = 3f;
					float mag = vector.Length();
					if (mag > speed)
					{
						mag = speed / mag;
						vector *= mag;
					}

					if (Projectile.owner == Main.myPlayer)
					{
						float numberProjectiles = 3;
						float rotation = MathHelper.ToRadians(20);
						for (int i = 0; i < numberProjectiles; i++)
						{
							Vector2 perturbedSpeed = new Vector2(vector.X, vector.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
							Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, perturbedSpeed, ModContent.ProjectileType<TotalityClickerPro2>(), (int)(Projectile.damage * 0.25f), Projectile.knockBack, Projectile.owner);
						}
					}
				}
				Timer = 0;
			}
		}
	}
}
