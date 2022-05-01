using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class NaughtyClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Presents = ClickerSystem.RegisterClickEffect(Mod, "Presents", null, null, 12, new Color(215, 150, 150), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < 4; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-6f, -4f), ModContent.ProjectileType<NaughtyClickerPro>(), damage, 0f, player.whoAmI, hasSpawnEffects);
					spawnEffects = false;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 5.8f);
			SetColor(Item, new Color(215, 150, 150));
			SetDust(Item, 5);
			AddEffect(Item, ClickEffect.Presents);

			Item.damage = 82;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 500000;
			Item.rare = 8;
		}
	}
}
