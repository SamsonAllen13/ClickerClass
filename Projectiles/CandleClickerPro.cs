using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ClickerClass.Items;

namespace ClickerClass.Projectiles
{
	public class CandleClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			isClickerProj = true;
			projectile.width = 8;
			projectile.height = 8;
			projectile.penetrate = -1;
			projectile.timeLeft = 600;
			projectile.alpha = 255;
			projectile.extraUpdates = 6;
		}

		public override void AI()
		{
			if (projectile.ai[0] > 0f)
			{
				projectile.extraUpdates = 0;
				for (int k = 0; k < 2; k++)
				{
					Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 55, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 255, default, 0.75f);
					dust.noGravity = true;
				}
			}
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (projectile.ai[0] < 1f)
			{
				projectile.timeLeft = 180;
				projectile.velocity = Vector2.Zero;
				projectile.ai[0] = 1f;
			}
			return false;
		}
	}
}