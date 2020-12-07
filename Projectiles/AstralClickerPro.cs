using ClickerClass.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class AstralClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 60;
			projectile.height = 60;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.timeLeft = 120;
			projectile.tileCollide = false;

			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			if (projectile.timeLeft > 4)
			{
				return new Color(255, 255, 255, 0) * 0.6f;
			}
			else
			{
				return new Color(255, 255, 255, 0) * 0f;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (projectile.timeLeft > 4)
			{
				spriteBatch.Draw(mod.GetTexture("Projectiles/AstralClickerPro"), projectile.Center - Main.screenPosition, null, new Color(255, 255, 255, 0) * 0.25f, projectile.ai[0], new Vector2(30, 30), 1.25f, SpriteEffects.None, 0f);
			}
			return true;
		}

		public override void AI()
		{
			projectile.rotation += 0.065f;
			projectile.ai[0] -= 0.15f;

			for (int k = 0; k < 1; k++)
			{
				Vector2 offset = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
				Dust dust = Dust.NewDustDirect(new Vector2(projectile.Center.X - 10, projectile.Center.Y - 10) + offset, 20, 20, ModContent.DustType<MiceDust>(), Scale: 1.5f);
				dust.noGravity = true;
				dust.velocity = -offset * 0.1f;
			}

			for (int u = 0; u < 200; u++)
			{
				NPC target = Main.npc[u];

				if (target.active && (target.type < 548 || target.type > 578) && target.type != NPCID.TargetDummy && !target.friendly && !target.boss && target.CanBeChasedBy(projectile, false) && Vector2.Distance(projectile.Center, target.Center) < 150)
				{
					float num3 = 11f;
					Vector2 vector = new Vector2(target.position.X + (float)(target.width / 2), target.position.Y + (float)(target.height / 2));
					float num4 = projectile.Center.X - vector.X;
					float num5 = projectile.Center.Y - vector.Y;
					float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
					num6 = num3 / num6;
					num4 *= num6;
					num5 *= num6;
					int num7 = 5;
					target.velocity.X = (target.velocity.X * (float)(num7 - 1) + num4) / (float)num7;
					target.velocity.Y = (target.velocity.Y * (float)(num7 - 1) + num5) / (float)num7;
				}
			}

			if (projectile.timeLeft == 4)
			{
				Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 122);

				for (int k = 0; k < 30; k++)
				{
					Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, ModContent.DustType<MiceDust>(), Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 0, default, 1.5f);
					dust.noGravity = true;
				}
				for (int k = 0; k < 20; k++)
				{
					Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 88, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 125, default, 1.15f);
					dust.noGravity = true;
				}
			}

			if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3)
			{
				projectile.velocity.X = 0f;
				projectile.velocity.Y = 0f;
				projectile.tileCollide = false;
				projectile.friendly = true;
				projectile.alpha = 255;
				projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
				projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
				projectile.width = 200;
				projectile.height = 200;
				projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
				projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
			}
		}
	}
}