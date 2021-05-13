using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Projectiles
{
	public class StickyKeychainPro : ClickerProjectile
	{
		public Vector2 location = Vector2.Zero;

		public int Frame
		{
			get => (int)projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		public bool HasLockedLocation
		{
			get => projectile.ai[1] == 1f;
			set => projectile.ai[1] = value ? 1f : 0f;
		}

		public bool Spawned
		{
			get => projectile.localAI[0] == 1f;
			set => projectile.localAI[0] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			projectile.width = 72;
			projectile.height = 72;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
			projectile.netImportant = true;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 200) * (0.005f * projectile.timeLeft);
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;

				Main.PlaySound(3, (int)projectile.Center.X, (int)projectile.Center.Y, 13);
			}

			Player player = Main.player[projectile.owner];
			projectile.frame = Frame;

			if (!HasLockedLocation)
			{
				location = player.Center - projectile.Center;
			}

			projectile.Center = player.Center - location;
			projectile.gfxOffY = player.gfxOffY;

			if (!HasLockedLocation)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust dust = Dust.NewDustDirect(projectile.Center - new Vector2(4), 8, 8, 88, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 175, default, 1.75f);
					dust.noGravity = true;
					dust.noLight = true;
				}
			}

			HasLockedLocation = true;
		}
	}
}
