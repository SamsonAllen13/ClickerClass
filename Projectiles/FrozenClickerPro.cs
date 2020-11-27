using ClickerClass.Buffs;
using Terraria;
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
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 10;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<Frozen>(), 120, false);
		}

		public override void Kill(int timeLeft)
		{
			Player player = Main.player[projectile.owner];

			for (int k = 0; k < 5; k++)
			{
				Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 15, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 255, default, 1.25f);
				dust.noGravity = true;
			}

			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			if (clickerPlayer.accEnchantedLED2)
			{
				for (int k = 0; k < 5; k++)
				{
					Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, 90, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1.15f);
					dust.noGravity = true;
				}
			}

			if (clickerPlayer.accEnchantedLED)
			{
				for (int i = 0; i < 15; i++)
				{
					int dustType = Main.rand.Next(3);
					switch (dustType)
					{
						case 0: dustType = 15; break;
						case 1: dustType = 57; break;
						default: dustType = 58; break;
					}
					Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, dustType, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 100, default, 1.5f);
					dust.velocity *= 1.5f;
					dust.noGravity = true;
				}
			}
		}
	}
}