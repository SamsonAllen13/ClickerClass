using ClickerClass.Dusts;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;

namespace ClickerClass.Projectiles
{
	public class AstralClickerPro : ClickerProjectile
	{
		public float SecondaryRotation
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public bool Spawned
		{
			get => Projectile.ai[1] == 1f;
			set => Projectile.ai[1] = value ? 1f : 0f;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 60;
			Projectile.height = 60;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 120;
			Projectile.tileCollide = false;

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			float alpha = 0f;
			if (Projectile.timeLeft > 4)
			{
				alpha = 0.6f;
			}
			return new Color(255, 255, 255, 0) * alpha;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			if (Projectile.timeLeft > 4)
			{
				Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center - Main.screenPosition, null, new Color(255, 255, 255, 0) * 0.25f, SecondaryRotation, new Vector2(30, 30), 1.25f, SpriteEffects.None, 0);
			}
			return true;
		}

		public override void AI()
		{
			Projectile.rotation += 0.065f;
			SecondaryRotation -= 0.15f;

			Vector2 offset = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
			Dust dust = Dust.NewDustDirect(new Vector2(Projectile.Center.X - 10, Projectile.Center.Y - 10) + offset, 20, 20, ModContent.DustType<MiceDust>(), Scale: 1.5f);
			dust.noGravity = true;
			dust.velocity = -offset * 0.1f;

			if (!Spawned)
			{
				Spawned = true;

				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 117);

				for (int k = 0; k < 20; k++)
				{
					dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, ModContent.DustType<MiceDust>(), Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 0, default, 1.25f);
					dust.noGravity = true;
				}
			}

			for (int u = 0; u < Main.maxNPCs; u++)
			{
				NPC target = Main.npc[u];

				if (target.CanBeChasedBy() && (target.type < NPCID.DD2EterniaCrystal || target.type > NPCID.DD2LightningBugT3) && !target.IsBossOrRelated() && Projectile.DistanceSQ(target.Center) < 150 * 150)
				{
					float mag = 11f;
					Vector2 center = target.Center;
					float x = Projectile.Center.X - center.X;
					float y = Projectile.Center.Y - center.Y;
					float len = (float)Math.Sqrt((double)(x * x + y * y));
					len = mag / len;
					x *= len;
					y *= len;
					int inertia = 5;
					target.velocity.X = (target.velocity.X * (float)(inertia - 1) + x) / (float)inertia;
					target.velocity.Y = (target.velocity.Y * (float)(inertia - 1) + y) / (float)inertia;
				}
			}

			if (Projectile.timeLeft == 4)
			{
				SoundEngine.PlaySound(2, (int)Projectile.Center.X, (int)Projectile.Center.Y, 122);

				for (int k = 0; k < 30; k++)
				{
					dust = Dust.NewDustDirect(Projectile.Center, 10, 10, ModContent.DustType<MiceDust>(), Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 0, default, 1.5f);
					dust.noGravity = true;
				}
				for (int k = 0; k < 20; k++)
				{
					dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 88, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 125, default, 1.15f);
					dust.noGravity = true;
				}
			}

			if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
			{
				Projectile.velocity.X = 0f;
				Projectile.velocity.Y = 0f;
				Projectile.tileCollide = false;
				Projectile.friendly = true;
				Projectile.alpha = 255;
				Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
				Projectile.width = 200;
				Projectile.height = 200;
				Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			}
		}
	}
}
