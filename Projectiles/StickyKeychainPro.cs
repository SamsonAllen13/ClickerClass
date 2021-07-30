using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;

namespace ClickerClass.Projectiles
{
	public class StickyKeychainPro : ClickerProjectile
	{
		public Vector2 location = Vector2.Zero;

		public int Frame
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public bool HasLockedLocation
		{
			get => Projectile.ai[1] == 1f;
			set => Projectile.ai[1] = value ? 1f : 0f;
		}

		public bool Spawned
		{
			get => Projectile.localAI[0] == 1f;
			set => Projectile.localAI[0] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 72;
			Projectile.height = 72;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
			Projectile.netImportant = true;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 200) * (0.005f * Projectile.timeLeft);
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;

				SoundEngine.PlaySound(3, (int)Projectile.Center.X, (int)Projectile.Center.Y, 13);
			}

			Player player = Main.player[Projectile.owner];
			Projectile.frame = Frame;

			if (!HasLockedLocation)
			{
				location = player.Center - Projectile.Center;
			}

			Projectile.Center = player.Center - location;
			Projectile.gfxOffY = player.gfxOffY;

			if (!HasLockedLocation)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 88, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 175, default, 1.75f);
					dust.noGravity = true;
					dust.noLight = true;
				}
			}

			HasLockedLocation = true;
		}
	}
}
