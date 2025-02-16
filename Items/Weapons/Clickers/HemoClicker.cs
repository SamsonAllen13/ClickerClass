using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class HemoClicker : ClickerWeapon
	{
		public static readonly int GlobuleAmount = 5;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Blood, projsInheritItemElements: true);

			ClickEffect.Linger = ClickerSystem.RegisterClickEffect(Mod, "Linger", 10, new Color(255, 50, 50), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < GlobuleAmount; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-3f, -1f), ModContent.ProjectileType<HemoClickerPro>(), damage / 2, 0f, player.whoAmI, hasSpawnEffects);
					spawnEffects = false;
				}
			},
			preHardMode: true,
			descriptionArgs: new object[] { GlobuleAmount });
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.75f);
			SetColor(Item, new Color(255, 50, 50));
			SetDust(Item, 5);
			AddEffect(Item, ClickEffect.Linger);

			Item.damage = 5;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 0, 26, 0);
			Item.rare = ItemRarityID.Blue;
		}
	}
}
