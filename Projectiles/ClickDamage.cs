using ClickerClass.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ClickerClass.Dusts;

namespace ClickerClass.Projectiles
{
	/// <summary>
	/// THE damage-inflicting clicker projectile spawned from a clicker. Default for a clicker weapon. Handles visuals
	/// </summary>
	public class ClickDamage : ClickerProjectile
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickerSystem.RegisterClickerWeaponProjectile(this);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = -1;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		bool spawned = false;

		public override void PostAI()
		{
			SpawnCircleDust();
		}

		public override void Kill(int timeLeft)
		{
			Player player = Main.player[Projectile.owner];
			Item item = player.HeldItem;

			int dustType = 0;
			if (ClickerSystem.IsClickerWeapon(item, out ClickerItemCore clickerItem))
			{
				dustType = clickerItem.clickerDustColor;
			}

			for (int k = 0; k < 5; k++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, dustType, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 75, default, 1f);
				dust.noGravity = true;
			}

			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			if (clickerPlayer.accEnchantedLED2)
			{
				for (int k = 0; k < 5; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 90, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1.15f);
					dust.noGravity = true;
				}
			}

			if (clickerPlayer.accEnchantedLED)
			{
				for (int i = 0; i < 15; i++)
				{
					int dustType1 = Main.rand.Next(3);
					switch (dustType1)
					{
						case 0: dustType1 = 15; break;
						case 1: dustType1 = 57; break;
						default: dustType1 = 58; break;
					}
					Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, dustType1, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 100, default, 1.5f);
					dust.velocity *= 1.5f;
					dust.noGravity = true;
				}
			}
		}

		private void SpawnCircleDust()
		{
			if (Main.netMode == NetmodeID.Server)
			{
				return;
			}

			if (Main.myPlayer == Projectile.owner)
			{
				//If self
				if (!ClickerConfigClient.Instance.ShowClickIndicator)
				{
					return;
				}
			}
			else
			{
				//If other client
				if (!ClickerConfigClient.Instance.ShowOthersClickIndicator)
				{
					return;
				}
			}

			if (!spawned)
			{
				spawned = true;

				Player player = Main.player[Projectile.owner];
				Item item = player.HeldItem;

				Color dustColor;
				if (ClickerSystem.IsClickerWeapon(item, out ClickerItemCore clickerItem))
				{
					dustColor = clickerItem.clickerRadiusColor;
				}
				else
				{
					return;
				}
				dustColor *= player.GetModPlayer<ClickerPlayer>().ClickerRadiusColorMultiplier;

				Vector2 vel = Vector2.UnitX * 2;

				int amount = 20;
				for (int i = 0; i < amount; i++)
				{
					float rot = MathHelper.TwoPi * i / amount;
					Vector2 velocity = vel.RotatedBy(rot);
					Dust dust = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<ColorableDust>(), velocity, newColor: dustColor, Alpha: 25);
					dust.scale = 1f;
				}
			}
		}
	}
}
