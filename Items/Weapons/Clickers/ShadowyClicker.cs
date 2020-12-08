using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
namespace ClickerClass.Items.Weapons.Clickers
{
	public class ShadowyClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Curse = ClickerSystem.RegisterClickEffect(mod, "Curse", null, null, 12, new Color(150, 100, 255), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 104);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 27, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 255, default, 1f);
					dust.noGravity = true;
				}

				Vector2 pos = Main.MouseWorld;

				int index = -1;
				for (int i = 0; i < 200; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.active && npc.CanBeChasedBy() && Vector2.DistanceSquared(pos, npc.Center) < 400f * 400f && Collision.CanHit(pos, 1, 1, npc.Center, 1, 1))
					{
						index = i;
					}
				}
				if (index != -1)
				{
					Vector2 vector = Main.npc[index].Center - pos;
					float speed = 6f;
					float mag = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
					if (mag > speed)
					{
						mag = speed / mag;
					}
					vector *= mag;
					Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, vector.X, vector.Y, ModContent.ProjectileType<ShadowyClickerPro>(), damage, knockBack, player.whoAmI);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.15f);
			SetColor(item, new Color(150, 100, 255));
			SetDust(item, 27);
			AddEffect(item, ClickEffect.Curse);

			item.damage = 12;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 15000;
			item.rare = 1;
		}
	}
}
