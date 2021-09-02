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
				Vector2 pos = Main.MouseWorld;

				int index = -1;
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.active && npc.CanBeChasedBy() && npc.DistanceSQ(pos) < 400f * 400f && Collision.CanHit(pos, 1, 1, npc.Center, 1, 1))
					{
						index = i;
					}
				}
				if (index != -1)
				{
					Vector2 vector = Main.npc[index].Center - pos;
					float speed = 6f;
					float mag = vector.Length();
					if (mag > speed)
					{
						mag = speed / mag;
					}
					vector *= mag;
					Projectile.NewProjectile(pos, vector, ModContent.ProjectileType<ShadowyClickerPro>(), damage, knockBack, player.whoAmI);
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

			item.damage = 7;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 15000;
			item.rare = 1;
		}
	}
}
