using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class NaughtyClickerPro : ClickerProjectile
	{
		public bool HasSpawnEffects
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public bool Trigger
		{
			get => Projectile.ai[1] == 1f;
			set => Projectile.ai[1] = value ? 1f : 0f;
		}

		public bool HasChanged
		{
			get => Projectile.ai[2] == 1f;
			set => Projectile.ai[2] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 46;
			Projectile.height = 46;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 1;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override void AI()
		{
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;
				SoundEngine.PlaySound(SoundID.Item61, Projectile.Center);
				for (int k = 0; k < 20; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 31, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 125, default, 2f);
					dust.noGravity = true;
				}
				for (int k = 0; k < 8; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 174, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1.25f);
					dust.noGravity = true;
				}
			}

			Projectile.velocity.X *= 0.99f;
			Projectile.velocity.Y += 0.05f;
			if (Projectile.velocity.Y > 0f)
			{
				Projectile.frame = 1;
				if (Projectile.velocity.Y > 4f)
				{
					Projectile.velocity.Y = 4f;
				}
			}

			if (Main.myPlayer == Projectile.owner && !HasChanged && Trigger)
			{
				HasChanged = true;
				Projectile.netUpdate = true;
			}

			if (HasChanged)
			{
				SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
				for (int k = 0; k < 10; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 31, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 125, default, 2f);
					dust.noGravity = true;
				}
				for (int k = 0; k < 5; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 174, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), 0, default, 1.25f);
					dust.noGravity = true;
				}

				if (Main.myPlayer == Projectile.owner)
				{
					for (int k = 0; k < 4; k++)
					{
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X + Main.rand.Next(-15, 16), Projectile.Center.Y), new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f)), ModContent.ProjectileType<NaughtyClickerPro2>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
					}
				}

				Projectile.Kill();
			}

			if (Projectile.timeLeft < 20)
			{
				Projectile.alpha += 8;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.velocity = Projectile.oldVelocity / 3;
			if (Projectile.timeLeft > 20)
			{
				Projectile.timeLeft = 20;
			}

			return false;
		}
	}
}
