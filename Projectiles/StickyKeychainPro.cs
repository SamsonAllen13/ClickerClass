using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class StickyKeychainPro : ClickerProjectile
	{
		public Vector2 location = Vector2.Zero;
		
		public override void SetDefaults()
		{
			isClickerProj = true;
			projectile.width = 72;
			projectile.height = 72;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
			projectile.friendly = true;
			projectile.tileCollide = false;
			Main.projFrames[projectile.type] = 3;
			
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 200) * (0.005f * projectile.timeLeft);
		}
		
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			projectile.frame = (int)(projectile.ai[0]);
			
			if (projectile.ai[1] < 1f)
			{
				location.X = player.Center.X - (projectile.Center.X - 36);
				location.Y = player.Center.Y - (projectile.Center.Y - 36);
				projectile.ai[1] += 1f;
			}
			
			projectile.position = player.Center - location;
		}
	}
}