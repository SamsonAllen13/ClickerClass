using ClickerClass.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;

namespace ClickerClass.Projectiles
{
	public class MindfulClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = -1;
			Projectile.alpha = 255;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 6;
		}
	
		public override void OnKill(int timeLeft)
		{
			for (int u = 0; u < Main.maxNPCs; u++)
			{
				NPC target = Main.npc[u];
				if (target.CanBeChasedBy() && target.DistanceSQ(Projectile.Center) < 44 * 44)
				{
					target.AddBuff(ModContent.BuffType<Charmed>(), 180, false);
					for (int i = 0; i < 10; i++)
					{
						int index = Dust.NewDust(target.position, target.width, target.height, 181, 0f, 0f, 100, default(Color), 1f);
						Dust dust = Main.dust[index];
						dust.noGravity = true;
						dust.noLight = true;
						dust.velocity *= 0.75f;
						int x = Main.rand.Next(-50, 51);
						int y = Main.rand.Next(-50, 51);
						dust.position.X += x;
						dust.position.Y += y;
						dust.velocity.X = -x * 0.075f;
						dust.velocity.Y = -y * 0.075f;
						dust.shader = GameShaders.Armor.GetSecondaryShader(37, Main.LocalPlayer);
					}
				}
			}
		}
	}
}
