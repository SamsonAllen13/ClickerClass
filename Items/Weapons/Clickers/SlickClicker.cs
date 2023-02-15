using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SlickClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Splash = ClickerSystem.RegisterClickEffect(Mod, "Splash", 6, new Color(75, 75, 255), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < 6; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-6f, -2f), ModContent.ProjectileType<SlickClickerPro>(), (int)(damage * 0.75f), knockBack, player.whoAmI, hasSpawnEffects);
					spawnEffects = false;
				}
			},
			preHardMode: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.45f);
			SetColor(Item, new Color(75, 75, 255));
			SetDust(Item, 33);
			AddEffect(Item, ClickEffect.Splash);

			Item.damage = 10;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 1, 75, 0);
			Item.rare = ItemRarityID.Green;
		}
	}
}
