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
		public float SecondaryRotation
		{
			get => projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		public bool Spawned
		{
			get => projectile.ai[1] == 1f;
			set => projectile.ai[1] = value ? 1f : 0f;
		}

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
			float alpha = 0f;
			if (projectile.timeLeft > 4)
			{
				alpha = 0.6f;
			}
			return new Color(255, 255, 255, 0) * alpha;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (projectile.timeLeft > 4)
			{
				spriteBatch.Draw(mod.GetTexture("Projectiles/AstralClickerPro"), projectile.Center - Main.screenPosition, null, new Color(255, 255, 255, 0) * 0.25f, SecondaryRotation, new Vector2(30, 30), 1.25f, SpriteEffects.None, 0f);
			}
			return true;
		}

		public override void AI()
		{
			projectile.rotation += 0.065f;
			SecondaryRotation -= 0.15f;

			Vector2 offset = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
			Dust dust = Dust.NewDustDirect(new Vector2(projectile.Center.X - 10, projectile.Center.Y - 10) + offset, 20, 20, ModContent.DustType<MiceDust>(), Scale: 1.5f);
			dust.noGravity = true;
			dust.velocity = -offset * 0.1f;

			if (!Spawned)
			{
				Spawned = true;

				Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 117);

				for (int k = 0; k < 20; k++)
				{
					dust = Dust.NewDustDirect(projectile.Center - new Vector2(4), 8, 8, ModContent.DustType<MiceDust>(), Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 0, default, 1.25f);
					dust.noGravity = true;
				}
			}

			for (int u = 0; u < Main.maxNPCs; u++)
			{
				NPC target = Main.npc[u];

				if (target.CanBeChasedBy() && (target.type < NPCID.DD2EterniaCrystal || target.type > NPCID.DD2LightningBugT3) && !target.boss && projectile.DistanceSQ(target.Center) < 150 * 150)
				{
					float mag = 11f;
					Vector2 center = target.Center;
					float x = projectile.Center.X - center.X;
					float y = projectile.Center.Y - center.Y;
					float len = (float)Math.Sqrt((double)(x * x + y * y));
					len = mag / len;
					x *= len;
					y *= len;
					int inertia = 5;
					target.velocity.X = (target.velocity.X * (float)(inertia - 1) + x) / (float)inertia;
					target.velocity.Y = (target.velocity.Y * (float)(inertia - 1) + y) / (float)inertia;
				}
			}

			if (projectile.timeLeft == 4)
			{
				Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 122);

				for (int k = 0; k < 30; k++)
				{
					dust = Dust.NewDustDirect(projectile.Center, 10, 10, ModContent.DustType<MiceDust>(), Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 0, default, 1.5f);
					dust.noGravity = true;
				}
				for (int k = 0; k < 20; k++)
				{
					dust = Dust.NewDustDirect(projectile.Center, 10, 10, 88, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 125, default, 1.15f);
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
