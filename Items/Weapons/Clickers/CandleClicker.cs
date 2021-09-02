using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CandleClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Illuminate = ClickerSystem.RegisterClickEffect(mod, "Illuminate", null, null, 10, new Color(255, 175, 75), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < 8; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), ModContent.ProjectileType<CandleClickerPro>(), 0, 0f, player.whoAmI, hasSpawnEffects);
					spawnEffects = false;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 1.5f);
			SetColor(item, new Color(255, 175, 75));
			SetDust(item, 55);
			AddEffect(item, ClickEffect.Illuminate);

			item.damage = 6;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 25000;
			item.rare = 2;
		}
	}
}
