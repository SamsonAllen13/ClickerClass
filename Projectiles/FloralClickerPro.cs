using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ClickerClass.Projectiles
{
	public class FloralClickerPro : ClickerProjectile
	{
		private UnifiedRandom rng;

		public UnifiedRandom Rng
		{
			get
			{
				if (rng == null)
				{
					rng = new UnifiedRandom(RandomSeed / (1 + Projectile.identity));
				}
				return rng;
			}
		}

		public int RandomSeed { get; set; }

		public int WobbleTimer { get; set; }

		public Vector2 HomeBase = Vector2.Zero;
		
		public bool Spawned
		{
			get => Projectile.ai[2] == 1f;
			set => Projectile.ai[2] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 4;
			
			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Nature);
		}

		public override void SetDefaults()
		{
			Projectile.width = 32;
			Projectile.height = 30;
			Projectile.aiStyle = -1;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 600;
			Projectile.netImportant = true;

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 20;
		}

		public override void OnSpawn(IEntitySource source)
		{
			RandomSeed = (int)DateTime.Now.Ticks;
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write7BitEncodedInt(RandomSeed);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			RandomSeed = reader.Read7BitEncodedInt();
		}
		
		public override void PostDraw(Color lightColor)
		{
			Vector2 center = HomeBase;
			Color color = Lighting.GetColor((int)center.X / 16, (int)((double)center.Y / 16.0));
			color = Projectile.GetAlpha(color);
			Main.EntitySpriteDraw(ModContent.Request<Texture2D>(Texture + "_Effect").Value, center - Main.screenPosition, null, color, 0f, new Vector2(13, 13), 1f, SpriteEffects.None, 0);
		}

		public override bool PreDraw(ref Color lightColor)
		{
			ProjectileExtras.DrawChain(Projectile.whoAmI, HomeBase, Texture + "_Chain", null, 0.8f * Projectile.Opacity);

			return true;
		}

		public override void AI()
		{
			HomeBase = Main.MouseWorld;
			Vector2 homeBase = HomeBase;
			
			Projectile.rotation = (Projectile.Center - homeBase).ToRotation() + MathHelper.PiOver2;
			
			if (!Spawned)
			{
				Spawned = true;
				SoundEngine.PlaySound(SoundID.Grass, Projectile.Center);
				for (int k = 0; k < 10; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 273, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 165, default, 1.5f);
					dust.noGravity = true;
					dust.noLight = true;
				}
			}
			
			WobbleTimer++;
			if (WobbleTimer % 4 == 0)
			{
				Projectile.velocity.Y += Rng.NextFloat(-0.5f, 0.5f);
				Projectile.velocity.X += Rng.NextFloat(-0.5f, 0.5f);
			}

			if (WobbleTimer > 20)
			{
				int num3;
				float num477 = Projectile.Center.X;
				float num478 = Projectile.Center.Y;
				float num479 = 400f;
				bool flag17 = false;
				float distSQ = Projectile.DistanceSQ(homeBase);

				for (int num480 = 0; num480 < Main.maxNPCs; num480 = num3 + 1)
				{
					if (Main.npc[num480].CanBeChasedBy() && distSQ < 280 * 280 && Projectile.DistanceSQ(Main.npc[num480].Center) < num479 * num479 && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[num480].Center, 1, 1))
					{
						float num481 = Main.npc[num480].position.X + (float)(Main.npc[num480].width / 2);
						float num482 = Main.npc[num480].position.Y + (float)(Main.npc[num480].height / 2);
						float num483 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num481) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num482);
						if (num483 < num479)
						{
							num479 = num483;
							num477 = num481;
							num478 = num482;
							flag17 = true;
						}
					}
					num3 = num480;
				}
				
				if (flag17)
				{
					Projectile.frameCounter++;
					if (Projectile.frameCounter > 3)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame >= Main.projFrames[Projectile.type])
					{
						Projectile.frame = 0;
					}
				}
				else
				{
					Projectile.frame = 0;
				}

				if (flag17 && distSQ < 250 * 250)
				{
					float num488 = 10f;

					Vector2 vector38 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
					float num489 = num477 - vector38.X;
					float num490 = num478 - vector38.Y;
					float num491 = (float)Math.Sqrt((double)(num489 * num489 + num490 * num490));
					if (num491 > 0)
					{
						num491 = num488 / num491;
					}
					num489 *= num491;
					num490 *= num491;

					Projectile.velocity.X = (Projectile.velocity.X * 20f + num489) / 21f;
					Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num490) / 21f;
					
					Projectile.extraUpdates = 0;
					Projectile.friendly = true;
				}
				else if (distSQ > 65 * 65)
				{
					float num488 = 12f;

					Vector2 vector38 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
					float num489 = homeBase.X - vector38.X;
					float num490 = homeBase.Y - vector38.Y;
					float num491 = (float)Math.Sqrt((double)(num489 * num489 + num490 * num490));
					if (num491 > 0)
					{
						num491 = num488 / num491;
					}
					num489 *= num491;
					num490 *= num491;

					Projectile.velocity.X = (Projectile.velocity.X * 20f + num489) / 21f;
					Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num490) / 21f;
					
					Projectile.extraUpdates = 1;
					Projectile.friendly = false;
				}
				else
				{
					Projectile.extraUpdates = 0;
					Projectile.friendly = true;
				}
			}
			
			if (Projectile.timeLeft < 40)
			{
				Projectile.alpha += 6;
			}
		}
	}
}
