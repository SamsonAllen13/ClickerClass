using ClickerClass.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class FrozenClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = -1;
			projectile.alpha = 255;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 6;
		}

		public override void Kill(int timeLeft)
		{
			Player player = Main.player[projectile.owner];
			for (int u = 0; u < Main.maxNPCs; u++)
			{
				NPC target = Main.npc[u];
				if (target.CanBeChasedBy(projectile) && target.DistanceSQ(projectile.Center) < 100 * 20)
				{
					target.AddBuff(ModContent.BuffType<Frozen>(), 120, false);
					for (int i = 0; i < 15; i++)
					{
						int num6 = Dust.NewDust(target.position, target.width, target.height, 15, 0f, 0f, 255, default(Color), 1f);
						Main.dust[num6].noGravity = true;
						Main.dust[num6].velocity *= 0.75f;
						int num7 = Main.rand.Next(-50, 51);
						int num8 = Main.rand.Next(-50, 51);
						Dust dust = Main.dust[num6];
						dust.position.X = dust.position.X + (float)num7;
						Dust dust2 = Main.dust[num6];
						dust2.position.Y = dust2.position.Y + (float)num8;
						Main.dust[num6].velocity.X = -(float)num7 * 0.075f;
						Main.dust[num6].velocity.Y = -(float)num8 * 0.075f;
					}
				}
			}
		}
	}
}