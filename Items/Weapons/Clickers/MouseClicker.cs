using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class MouseClicker : ClickerWeapon
	{
		public static readonly int MouseTrapCount = 3;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Trap = ClickerSystem.RegisterClickEffect(Mod, "Trap", 10, new Color(120, 120, 120), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				int trapType = ModContent.ProjectileType<MouseClickerPro>();

				//Despawn oldest not-attached traps above threshold
				List<Projectile> list = new();
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					Projectile proj = Main.projectile[i];

					if (proj.active && proj.type == trapType && proj.ModProjectile is MouseClickerPro mouseClicker &&
						mouseClicker.StuckState == 0)
					{
						list.Add(proj);
					}
				}

				list.Sort((p1, p2) => p2.timeLeft - p1.timeLeft);
				int limit = 15 - MouseTrapCount;
				if (list.Count >= limit)
				{
					for (int i = limit; i < list.Count; i++)
					{
						list[i].Kill();
					}
				}

				bool spawnEffects = true;
				for (int k = 0; k < MouseTrapCount; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-5f, -2f), trapType, damage / 2, 0f, player.whoAmI, ai1: hasSpawnEffects);
					spawnEffects = false;
				}
			},
			descriptionArgs: new object[] { MouseTrapCount });
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 5.5f);
			SetColor(Item, new Color(120, 120, 120));
			SetDust(Item, 5);
			AddEffect(Item, ClickEffect.Trap);

			Item.damage = 54;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}
	}
}
