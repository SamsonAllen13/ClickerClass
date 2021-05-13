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
			for (int u = 0; u < Main.maxNPCs; u++)
			{
				NPC target = Main.npc[u];
				if (target.CanBeChasedBy() && target.DistanceSQ(projectile.Center) < 44 * 44)
				{
					target.AddBuff(ModContent.BuffType<Frozen>(), 120, false);
					for (int i = 0; i < 15; i++)
					{
						int index = Dust.NewDust(target.position, target.width, target.height, 15, 0f, 0f, 255, default(Color), 1f);
						Dust dust = Main.dust[index];
						dust.noGravity = true;
						dust.velocity *= 0.75f;
						int x = Main.rand.Next(-50, 51);
						int y = Main.rand.Next(-50, 51);
						dust.position.X += x;
						dust.position.Y += y;
						dust.velocity.X = -x * 0.075f;
						dust.velocity.Y = -y * 0.075f;
					}
				}
			}
		}
	}
}
