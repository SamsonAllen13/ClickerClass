using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class HemoClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Linger = ClickerSystem.RegisterClickEffect(mod, "Linger", null, null, 10, new Color(255, 50, 50), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < 5; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-3f, -1f), ModContent.ProjectileType<HemoClickerPro>(), damage / 2, 0f, player.whoAmI, hasSpawnEffects);
					spawnEffects = false;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 1.75f);
			SetColor(item, new Color(255, 50, 50));
			SetDust(item, 5);
			AddEffect(item, ClickEffect.Linger);

			item.damage = 6;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 13000;
			item.rare = 1;
		}
	}
}
