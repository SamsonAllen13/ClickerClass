using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class HemoClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Linger = ClickerSystem.RegisterClickEffect(Mod, "Linger", null, null, 10, new Color(255, 50, 50), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < 5; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-3f, -1f), ModContent.ProjectileType<HemoClickerPro>(), damage / 2, 0f, player.whoAmI, hasSpawnEffects);
					spawnEffects = false;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.75f);
			SetColor(Item, new Color(255, 50, 50));
			SetDust(Item, 5);
			AddEffect(Item, ClickEffect.Linger);

			Item.damage = 6;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 13000;
			Item.rare = 1;
		}
	}
}
