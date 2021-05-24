using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class MyceliumClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Spores = ClickerSystem.RegisterClickEffect(mod, "Spores", null, null, 10, new Color(115, 150, 220), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < 5; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-4f, -2f), ModContent.ProjectileType<MyceliumClickerPro>(), (int)(damage * 0.5f), 2f, player.whoAmI, hasSpawnEffects);
					spawnEffects = false;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 1.6f);
			SetColor(item, new Color(115, 150, 220));
			SetDust(item, 224);
			AddEffect(item, ClickEffect.Spores);

			item.damage = 9;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 10000;
			item.rare = 1;
		}
	}
}
