using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class UmbralClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.ShadowLash = ClickerSystem.RegisterClickEffect(Mod, "ShadowLash", null, null, 12, new Color(150, 100, 255), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < 5; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), ModContent.ProjectileType<UmbralClickerPro>(), (int)(damage * 0.75f), knockBack, player.whoAmI, (int)DateTime.Now.Ticks, hasSpawnEffects);
					spawnEffects = false;
				}
			},
			preHardMode: true);
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
			Item.value = 200000;
			Item.rare = 3;
		}
	}
}
