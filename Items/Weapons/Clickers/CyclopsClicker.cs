using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CyclopsClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Insanity = ClickerSystem.RegisterClickEffect(Mod, "Insanity", null, null, 8, new Color(75, 75, 75), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < 1; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					int direction = player.direction;
					Projectile.NewProjectile(source, position.X, position.Y, 0f, 0f, ModContent.ProjectileType<CyclopsClickerPro>(), damage, 0f, player.whoAmI, hasSpawnEffects, direction);
					spawnEffects = false;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.25f);
			SetColor(Item, new Color(125, 125, 125));
			SetDust(Item, 54);
			AddEffect(Item, ClickEffect.Insanity);

			Item.damage = 10;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 87500;
			Item.rare = 2;
		}
	}
}
