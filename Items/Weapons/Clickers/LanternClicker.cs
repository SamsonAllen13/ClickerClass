using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class LanternClicker : ClickerWeapon
	{
		public static readonly int SparkAmount = 8;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Fire, projsInheritItemElements: true);

			ClickEffect.Incinerate = ClickerSystem.RegisterClickEffect(Mod, "Incinerate", 15, new Color(255, 155, 65), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < SparkAmount; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(3f, 6f), ModContent.ProjectileType<LanternClickerPro>(), (int)(damage * 0.1f), 1f, player.whoAmI, hasSpawnEffects);
					spawnEffects = false;
				}
			},
			descriptionArgs: new object[] { SparkAmount });
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 5.9f);
			SetColor(Item, new Color(200, 125, 75));
			SetDust(Item, 174);
			AddEffect(Item, ClickEffect.Incinerate);

			Item.damage = 60;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}
	}
}
