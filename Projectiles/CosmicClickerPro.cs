using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class CosmicClickerPro : ClickerProjectile
	{
		public bool Spawned
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 4;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;

			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Fire);
			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Shadow);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 52;
			Projectile.height = 52;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 180;
			AIType = ProjectileID.Bullet;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 180;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return (new Color(255, 255, 255, 255) * 1f) * Projectile.Opacity;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
			Rectangle frame = texture2D.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				float scaleDown = (1f / ProjectileID.Sets.TrailCacheLength[Projectile.type]) * (ProjectileID.Sets.TrailCacheLength[Projectile.type] - k);
				float alphaDown = (.5f / ProjectileID.Sets.TrailCacheLength[Projectile.type]) * (ProjectileID.Sets.TrailCacheLength[Projectile.type] - k);
				lightColor = new Color(255, 255, 255, 255);
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				if (k % 3 == 0)
				{
					Main.EntitySpriteDraw(texture2D, drawPos, frame, ((lightColor * alphaDown) * 0.5f) * Projectile.Opacity, Projectile.rotation, drawOrigin, scaleDown, SpriteEffects.None, 0);
				}
			}
			return true;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.OnFire3, 120, false);
		}
		
		public override bool ShouldUpdatePosition()
		{
			if (Projectile.ai[1] > 0)
			{
				Projectile.position = Main.MouseWorld - new Vector2(26, 26);
				return false;
			}
			return true;
		}
		
		public override bool PreAI()
		{
			if (Projectile.ai[1] > 0)
			{
				Projectile.alpha = 255;
				Projectile.ai[1]--;
				return false;
			}
			
			Projectile.alpha = 0;
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			if (!Spawned)
			{
				Spawned = true;

				SoundEngine.PlaySound(SoundID.Item100 with { PitchVariance = .5f }, Projectile.Center);
				for (int k = 0; k < 10; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 174, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 0, default, 1f);
					dust.noGravity = true;
				}
				
				Vector2 pos = Projectile.Center;

				int index = -1;
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.active && npc.CanBeChasedBy() && npc.DistanceSQ(pos) < 1000f * 1000f && Collision.CanHit(pos, 1, 1, npc.Center, 1, 1))
					{
						index = i;
					}
				}
				Vector2 vector = index == -1 ? pos - player.Center : Main.npc[index].Center - pos;
				
				float speed = 10f;
				float mag = vector.Length();
				if (mag > speed)
				{
					mag = speed / mag;
					vector *= mag;
				}
				
				Vector2 perturbedSpeed = vector.RotatedByRandom(MathHelper.ToRadians(8));
				vector = perturbedSpeed;
				Projectile.velocity = vector;
			}

			Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;

			if (Main.rand.Next(2) == 0)
			{
				int DustID = Dust.NewDust(Projectile.Center, Projectile.width / 3, Projectile.height / 3, 174, 0f, 0f, 0, default(Color), 1f);
				Main.dust[DustID].noGravity = true;
				int DustID2 = Dust.NewDust(Projectile.Center, Projectile.width / 3, Projectile.height / 3, 127, 0f, 0f, 0, default(Color), 1f);
				Main.dust[DustID2].noGravity = true;
			}
			
			Projectile.ai[2]++;
			if (Projectile.ai[2] > 10)
			{
				float num102 = 20f;
				int num103 = 0;
				while ((float)num103 < num102)
				{
					Vector2 vector12 = Vector2.UnitX * 0f;
					vector12 += -Vector2.UnitY.RotatedBy((double)((float)num103 * (6.28318548f / num102)), default(Vector2)) * new Vector2(2f, 6f);
					vector12 = vector12.RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
					int num104 = Dust.NewDust(Projectile.Center, 0, 0, 6, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num104].scale = 1.5f;
					Main.dust[num104].noGravity = true;
					Main.dust[num104].position = Projectile.Center + vector12;
					Main.dust[num104].velocity = Projectile.velocity * 0f + vector12.SafeNormalize(Vector2.UnitY) * 1f;
					int num = num103;
					num103 = num + 1;
				}
				Projectile.ai[2] = 0;
			}
			
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 4)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= 4)
			{
				Projectile.frame = 0;
				return;
			}
		}

		public override void OnKill(int timeLeft)
		{
			for (int u = 0; u < 20; u++)
			{
				int dust = Dust.NewDust(Projectile.Center, 10, 10, 174, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 0, default(Color), 1.25f);
				Main.dust[dust].noGravity = true;
				int dust2 = Dust.NewDust(Projectile.Center, 10, 10, 127, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 0, default(Color), 2f);
				Main.dust[dust2].noGravity = true;
			}
		}
	}
}
