using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class HighTechClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Discharge = ClickerSystem.RegisterClickEffect(mod, "Discharge", null, null, 10, new Color(75, 255, 200), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < 4; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), ModContent.ProjectileType<HighTechClickerPro>(), damage, 0f, player.whoAmI, (int)DateTime.Now.Ticks, hasSpawnEffects);
					spawnEffects = false;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 6f);
			SetColor(item, new Color(75, 255, 200));
			SetDust(item, 226);
			AddEffect(item, ClickEffect.Discharge);

			item.damage = 90;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 500000;
			item.rare = 8;
		}
	}
}
