using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CandleClicker : ClickerWeapon
	{
		public static readonly int SparkAmount = 8;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Illuminate = ClickerSystem.RegisterClickEffect(Mod, "Illuminate", 10, new Color(255, 175, 75), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < SparkAmount; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), ModContent.ProjectileType<CandleClickerPro>(), 0, 0f, player.whoAmI, hasSpawnEffects);
					spawnEffects = false;
				}
			},
			preHardMode: true,
			descriptionArgs: new object[] { SparkAmount });
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.5f);
			SetColor(Item, new Color(255, 175, 75));
			SetDust(Item, 55);
			AddEffect(Item, ClickEffect.Illuminate);

			Item.damage = 6;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 0, 50, 0);
			Item.rare = ItemRarityID.Green;
		}
	}
}
