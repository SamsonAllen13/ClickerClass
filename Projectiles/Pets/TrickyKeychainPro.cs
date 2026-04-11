using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles.Pets
{
	public class TrickyKeychainPro : ModProjectile
	{
		public int animateIdleMax = 3;
		public int animateIdle = 0;
		public int animateFall = 3;

		public int animateWalkMin = 4;
		public int animateWalkMax = 7;

		public int animateFlyMin = 8;
		public int animateFlyMax = 8;

		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 9;
			Main.projPet[Projectile.type] = true;
			
			ProjectileID.Sets.CharacterPreviewAnimations[Projectile.type] = ProjectileID.Sets.SimpleLoop(animateWalkMin, animateWalkMax + 1 - animateWalkMin, 3)
				.WhenNotSelected(6, 0)
				.WithOffset(-10, 0)
				.WithSpriteDirection(-1);
		}

		public override void SetDefaults()
		{
			Projectile.width = 42;
			Projectile.height = 42;
			Projectile.aiStyle = -1;
			Projectile.manualDirectionChange = true;

			DrawOriginOffsetY = -3;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			
			/*
			ThoriumPlayer modPlayer = player.GetThoriumPlayer();
			if (player.dead)
			{
				modPlayer.petRat = false;
			}
			if (modPlayer.petRat)
			{
				Projectile.timeLeft = 2;
			}
			*/

			if (!Main.player[Projectile.owner].active)
			{
				Projectile.active = false;
				return;
			}

			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			int num = 85;

			if (Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) < Projectile.position.X + (float)(Projectile.width / 2) - (float)num)
			{
				flag = true;
			}
			else if (Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) > Projectile.position.X + (float)(Projectile.width / 2) + (float)num)
			{
				flag2 = true;
			}

			if (Projectile.ai[1] == 0f)
			{
				int num37 = 500;

				if (Main.player[Projectile.owner].rocketDelay2 > 0)
				{
					Projectile.ai[0] = 1f;
				}
				Vector2 vector6 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num38 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector6.X;
				float num39 = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector6.Y;
				float num40 = (float)Math.Sqrt((double)(num38 * num38 + num39 * num39));
				if (num40 > 2000f)
				{
					Projectile.position.X = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - (float)(Projectile.width / 2);
					Projectile.position.Y = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - (float)(Projectile.height / 2);
				}
				else if (num40 > (float)num37 || (Math.Abs(num39) > 300f && Projectile.localAI[0] <= 0f))
				{
					Projectile.ai[0] = 1f;
				}
			}

			if (Projectile.ai[0] != 0f)
			{
				float num41 = 0.2f;
				int num42 = 200;
				Projectile.tileCollide = false;
				Vector2 vector7 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num43 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector7.X;
				float num49 = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector7.Y;
				float num50 = (float)Math.Sqrt((double)(num43 * num43 + num49 * num49));
				float num51 = 10f;
				float num52 = num50;
				if (num50 < (float)num42 && Main.player[Projectile.owner].velocity.Y == 0f && Projectile.position.Y + (float)Projectile.height <= Main.player[Projectile.owner].position.Y + (float)Main.player[Projectile.owner].height && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
				{
					Projectile.ai[0] = 0f;
					if (Projectile.velocity.Y < -6f)
					{
						Projectile.velocity.Y = -6f;
					}
				}
				if (num50 < 60f)
				{
					num43 = Projectile.velocity.X;
					num49 = Projectile.velocity.Y;
				}
				else
				{
					num50 = num51 / num50;
					num43 *= num50;
					num49 *= num50;
				}

				if (Projectile.velocity.X < num43)
				{
					Projectile.velocity.X = Projectile.velocity.X + num41;
					if (Projectile.velocity.X < 0f)
					{
						Projectile.velocity.X = Projectile.velocity.X + num41 * 1.5f;
					}
				}
				if (Projectile.velocity.X > num43)
				{
					Projectile.velocity.X = Projectile.velocity.X - num41;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.velocity.X = Projectile.velocity.X - num41 * 1.5f;
					}
				}
				if (Projectile.velocity.Y < num49)
				{
					Projectile.velocity.Y = Projectile.velocity.Y + num41;
					if (Projectile.velocity.Y < 0f)
					{
						Projectile.velocity.Y = Projectile.velocity.Y + num41 * 1.5f;
					}
				}
				if (Projectile.velocity.Y > num49)
				{
					Projectile.velocity.Y = Projectile.velocity.Y - num41;
					if (Projectile.velocity.Y > 0f)
					{
						Projectile.velocity.Y = Projectile.velocity.Y - num41 * 1.5f;
					}
				}

				int num9 = Projectile.frameCounter;
				Projectile.frameCounter = num9 + 1;
				if (Projectile.frameCounter > 1)
				{
					num9 = Projectile.frame;
					Projectile.frame = num9 + 1;
					Projectile.frameCounter = 0;
				}
				if (Projectile.frame < animateFlyMin || Projectile.frame > animateFlyMax)
				{
					Projectile.frame = animateFlyMin;
				}
				Projectile.rotation = 0f;
				Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
				if (Projectile.direction == -1)
				{
					Projectile.spriteDirection = 1;
				}
				if (Projectile.direction == 1)
				{
					Projectile.spriteDirection = -1;
				}
			}
			else
			{
				Vector2 vector9 = Vector2.Zero;
				bool flag8 = false;

				if (Projectile.ai[1] != 0f)
				{
					flag = false;
					flag2 = false;
				}

				if (!flag8)
				{
					Projectile.rotation = 0f;
				}

				float num104 = 0.075f;
				float num105 = 75f;
				Projectile.tileCollide = true;

				if (flag)
				{
					if ((double)Projectile.velocity.X > -3.5)
					{
						Projectile.velocity.X = Projectile.velocity.X - num104;
					}
					else
					{
						Projectile.velocity.X = Projectile.velocity.X - num104 * 0.25f;
					}
				}
				else if (flag2)
				{
					if ((double)Projectile.velocity.X < 3.5)
					{
						Projectile.velocity.X = Projectile.velocity.X + num104;
					}
					else
					{
						Projectile.velocity.X = Projectile.velocity.X + num104 * 0.25f;
					}
				}
				else
				{
					Projectile.velocity.X = Projectile.velocity.X * 0.9f;
					if (Projectile.velocity.X >= -num104 && Projectile.velocity.X <= num104)
					{
						Projectile.velocity.X = 0f;
					}
				}

				if (flag | flag2)
				{
					int num106 = (int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16;
					int j2 = (int)(Projectile.position.Y + (float)(Projectile.height / 2)) / 16;
					if (flag)
					{
						int num9 = num106;
						num106 = num9 - 1;
					}
					if (flag2)
					{
						int num9 = num106;
						num106 = num9 + 1;
					}
					num106 += (int)Projectile.velocity.X;
					if (WorldGen.SolidTile(num106, j2))
					{
						flag4 = true;
					}
				}
				if (Main.player[Projectile.owner].position.Y + (float)Main.player[Projectile.owner].height - 8f > Projectile.position.Y + (float)Projectile.height)
				{
					flag3 = true;
				}
				Collision.StepUp(ref Projectile.position, ref Projectile.velocity, Projectile.width, Projectile.height, ref Projectile.stepSpeed, ref Projectile.gfxOffY, 1, false, 0);
				if (Projectile.velocity.Y == 0f)
				{
					if (!flag3 && (Projectile.velocity.X < 0f || Projectile.velocity.X > 0f))
					{
						int num107 = (int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16;
						int j3 = (int)(Projectile.position.Y + (float)(Projectile.height / 2)) / 16 + 1;
						if (flag)
						{
							int num9 = num107;
							num107 = num9 - 1;
						}
						if (flag2)
						{
							int num9 = num107;
							num107 = num9 + 1;
						}
						WorldGen.SolidTile(num107, j3);
					}
					if (flag4)
					{
						int num108 = (int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16;
						int num109 = (int)(Projectile.position.Y + (float)Projectile.height) / 16 + 1;
						if (WorldGen.SolidTile(num108, num109) || Main.tile[num108, num109].IsHalfBlock || Main.tile[num108, num109].Slope > 0)
						{
							try
							{
								num108 = (int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16;
								num109 = (int)(Projectile.position.Y + (float)(Projectile.height / 2)) / 16;
								if (flag)
								{
									int num9 = num108;
									num108 = num9 - 1;
								}
								if (flag2)
								{
									int num9 = num108;
									num108 = num9 + 1;
								}
								num108 += (int)Projectile.velocity.X;
								if (!WorldGen.SolidTile(num108, num109 - 1) && !WorldGen.SolidTile(num108, num109 - 2))
								{
									Projectile.velocity.Y = -5.1f;
								}
								else if (!WorldGen.SolidTile(num108, num109 - 2))
								{
									Projectile.velocity.Y = -7.1f;
								}
								else if (WorldGen.SolidTile(num108, num109 - 5))
								{
									Projectile.velocity.Y = -11.1f;
								}
								else if (WorldGen.SolidTile(num108, num109 - 4))
								{
									Projectile.velocity.Y = -10.1f;
								}
								else
								{
									Projectile.velocity.Y = -9.1f;
								}
							}
							catch
							{
								Projectile.velocity.Y = -9.1f;
							}
						}
					}
				}
				if (Projectile.velocity.X > num105)
				{
					Projectile.velocity.X = num105;
				}
				if (Projectile.velocity.X < -num105)
				{
					Projectile.velocity.X = -num105;
				}

				if (Projectile.velocity.X != 0f) Projectile.direction = (Projectile.velocity.X > 0f).ToDirectionInt();
				if (Projectile.velocity.X > num104 & flag2)
				{
					Projectile.direction = 1;
				}
				if (Projectile.velocity.X < -num104 & flag)
				{
					Projectile.direction = -1;
				}

				if (Projectile.direction == -1)
				{
					Projectile.spriteDirection = 1;
				}
				if (Projectile.direction == 1)
				{
					Projectile.spriteDirection = -1;
				}

				if (Projectile.velocity.Y == 0f) // Idle
				{
					if (Projectile.velocity.X == 0f)
					{
						Projectile.frameCounter++;
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						
						if (Projectile.frame < animateIdle || Projectile.frame > animateIdleMax)
						{
							Projectile.frame = animateIdle;
						}
					}
					else if ((double)Projectile.velocity.X < -0.8 || (double)Projectile.velocity.X > 0.8) // Walking
					{
						Projectile.frameCounter += (int)Math.Abs((double)Projectile.velocity.X * 0.75);
						int num9 = Projectile.frameCounter;
						Projectile.frameCounter = num9 + 1;
						if (Projectile.frameCounter > 6)
						{
							num9 = Projectile.frame;
							Projectile.frame = num9 + 1;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame >= animateWalkMax || Projectile.frame < animateWalkMin)
						{
							Projectile.frame = animateWalkMin;
						}
					}
					else if (Projectile.frame > 0)
					{
						Projectile.frameCounter += 2;
						if (Projectile.frameCounter > 6)
						{
							int num9 = Projectile.frame;
							Projectile.frame = num9 + 1;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame >= animateWalkMax)
						{
							Projectile.frame = 1;
						}
					}
					else // Idle failsafe
					{
						Projectile.frame = animateIdle;
						Projectile.frameCounter = 0;
					}
				}
				else if (Projectile.velocity.Y < 0f || Projectile.velocity.Y > 0f) // Falling
				{
					Projectile.frameCounter = 0;
					Projectile.frame = animateFall;
				}

				Projectile.velocity.Y = Projectile.velocity.Y + 0.4f;
				if (Projectile.velocity.Y > 10f)
				{
					Projectile.velocity.Y = 10f;
				}
			}
		}
	}
}