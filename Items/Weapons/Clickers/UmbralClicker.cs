using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class UmbralClicker : ClickerWeapon
	{
		public static readonly int ShadowAmount = 5;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Shadow, projsInheritItemElements: true);

			ClickEffect.ShadowLash = ClickerSystem.RegisterClickEffect(Mod, "ShadowLash", 12, new Color(150, 100, 255), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < ShadowAmount; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), ModContent.ProjectileType<UmbralClickerPro>(), (int)(damage * 0.5f), knockBack, player.whoAmI, (int)DateTime.Now.Ticks, hasSpawnEffects);
					spawnEffects = false;
				}
			},
			preHardMode: true,
			descriptionArgs: new object[] { ShadowAmount });
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.75f);
			SetColor(Item, new Color(150, 100, 255));
			SetDust(Item, 27);
			AddEffect(Item, ClickEffect.ShadowLash);

			Item.damage = 14;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(0, 4, 0, 0);
			Item.rare = ItemRarityID.Orange;
		}
	}
}
