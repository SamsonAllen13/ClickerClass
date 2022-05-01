using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using ReLogic.Content;
using ClickerClass.Buffs;
using ClickerClass.NPCs;

namespace ClickerClass.Projectiles
{
	public class ClearKeychainPro : ClickerProjectile
	{
		public Vector2 Location => new Vector2(Projectile.ai[0], Projectile.ai[1]);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 300;
			Projectile.extraUpdates = 1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			if (Projectile.timeLeft > 4)
			{
				Asset<Texture2D> asset = TextureAssets.Projectile[Projectile.type];
				Vector2 drawOrigin = new Vector2(asset.Width() * 0.5f, Projectile.height * 0.5f);
				for (int k = 0; k < Projectile.oldPos.Length; k++)
				{
					Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
					Color color = Projectile.GetAlpha(lightColor * 0.25f) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
					Main.EntitySpriteDraw(asset.Value, drawPos, null, color * 0.25f, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
				}
			}
			return true;
		}

		public override void AI()
		{
			Projectile.rotation += Projectile.velocity.X > 0f ? 0.35f : -0.35f;

			for (int l = 0; l < 2; l++)
			{
				int num235 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y) - Projectile.velocity, Projectile.width, Projectile.height, 59, 0f, 0f, 150, default(Color), 1f);
				Dust dust4 = Main.dust[num235];
				dust4.velocity *= 0f;
				Main.dust[num235].noGravity = true;
			}

			Vector2 vec = Location;
			if (Projectile.DistanceSQ(vec) <= 10 * 10)
			{
				SoundEngine.PlaySound(2, (int)Projectile.Center.X, (int)Projectile.Center.Y, 86);
				Projectile.timeLeft = 4;

				for (int k = 0; k < 10; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 59, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 125, default, 1.5f);
					dust.noGravity = true;
				}
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 103, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 100, default, 1.75f);
					dust.noGravity = true;
				}
				
				for (int u = 0; u < Main.maxNPCs; u++)
				{
					NPC target = Main.npc[u];
					if (target.CanBeChasedBy() && !target.GetGlobalNPC<ClickerGlobalNPC>().crystalSlimeFatigue && target.DistanceSQ(Projectile.Center) < 200 * 200)
					{
						target.AddBuff(ModContent.BuffType<Crystalized>(), 300, false);
						for (int i = 0; i < 15; i++)
						{
							int index = Dust.NewDust(target.position, target.width, target.height, 71, 0f, 0f, 255, default(Color), 1f);
							Dust dust = Main.dust[index];
							dust.noGravity = true;
							dust.velocity *= 0.75f;
							int x = Main.rand.Next(-50, 51);
							int y = Main.rand.Next(-50, 51);
							dust.position.X += x;
							dust.position.Y += y;
							dust.velocity.X = -x * 0.075f;
							dust.velocity.Y = -y * 0.075f;
						}
					}
				}

				Projectile.Kill();
			}
		}
	}
}
