using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class MyceliumClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Spores = ClickerSystem.RegisterClickEffect(Mod, "Spores", null, null, 10, new Color(115, 150, 220), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < 5; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-4f, -2f), ModContent.ProjectileType<MyceliumClickerPro>(), (int)(damage * 0.5f), 2f, player.whoAmI, hasSpawnEffects);
					spawnEffects = false;
				}
			},
			preHardMode: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.6f);
			SetColor(Item, new Color(115, 150, 220));
			SetDust(Item, 224);
			AddEffect(Item, ClickEffect.Spores);

			Item.damage = 6;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 10000;
			Item.rare = 1;
		}
	}
}
