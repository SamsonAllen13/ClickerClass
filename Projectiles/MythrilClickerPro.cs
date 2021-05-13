using ClickerClass.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class MythrilClickerPro : ClickerProjectile
	{
		public bool Spawned
		{
			get => projectile.ai[0] == 1f;
			set => projectile.ai[0] = value ? 1f : 0f;
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.penetrate = -1;
			projectile.timeLeft = 10;
			projectile.alpha = 255;
			projectile.extraUpdates = 3;
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;

				Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 101);

				for (int k = 0; k < 30; k++)
				{
					Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 57, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 0, default, 1.35f);
					dust.shader = GameShaders.Armor.GetSecondaryShader(20, Main.LocalPlayer);
					dust.noGravity = true;
				}

				for (int u = 0; u < Main.maxNPCs; u++)
				{
					NPC target = Main.npc[u];
					if (target.CanBeChasedBy() && target.DistanceSQ(projectile.Center) < 250 * 250)
					{
						target.AddBuff(ModContent.BuffType<Embrittle>(), 240, false);

						for (int i = 0; i < 15; i++)
						{
							int index = Dust.NewDust(target.position, target.width, target.height, 57, 0f, 0f, 100, default(Color), 1f);
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
}
