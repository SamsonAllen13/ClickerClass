using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class DraconicClickerPro : ClickerProjectile
	{
		public float FlightStage
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public float FlightTimer
		{
			get => Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public int AttackTimer
		{
			get => (int)Projectile.localAI[0];
			set => Projectile.localAI[0] = value;
		}

		public bool Spawned
		{
			get => Projectile.localAI[1] == 1f;
			set => Projectile.localAI[1] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;

			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Fire, projsInheritProjElements: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 44;
			Projectile.height = 44;
			Projectile.aiStyle = -1;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 260;
			Projectile.alpha = 255;
			Projectile.extraUpdates = 2;
			Projectile.tileCollide = false;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
			Vector2 drawOrigin = new Vector2(texture2D.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha((lightColor * 0.25f) * Projectile.Opacity) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture2D, drawPos, null, color * 0.1f, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}

		public override void AI()
		{
			Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

			if (!Spawned)
			{
				Spawned = true;
				SoundEngine.PlaySound(SoundID.Item74, Projectile.Center);
				for (int k = 0; k < 30; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 31, Main.rand.NextFloat(-9f, 9f), Main.rand.NextFloat(-9f, 9f), 125, default, 2f);
					dust.noGravity = true;
				}
				for (int k = 0; k < 10; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 174, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 0, default, 1.25f);
					dust.noGravity = true;
				}
			}

			if (FlightStage == 0f)
			{
				Projectile.alpha -= 10;
				FlightTimer += 0.05f;
				if (FlightTimer > 3f)
				{
					FlightStage = 1f;
				}
			}
			else if (FlightStage == 1f)
			{
				FlightTimer -= 0.05f;
				if (FlightTimer < -3f)
				{
					FlightStage = 2f;
				}
			}
			else
			{
				Projectile.alpha += 10;
				FlightTimer += 0.05f;
			}

			Projectile.velocity.Y = FlightTimer;

			AttackTimer++;
			if (AttackTimer > 5 && Projectile.timeLeft > 20)
			{
				if (Main.myPlayer == Projectile.owner)
				{
					float direction = Projectile.velocity.X > 0f ? 15f : -15f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.Center.Y + 16), new Vector2(direction, 12f), ModContent.ProjectileType<DraconicClickerPro2>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
				}
				AttackTimer = 0;
			}
		}
	}
}
