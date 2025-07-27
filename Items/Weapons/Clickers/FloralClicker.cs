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
	public class FloralClicker : ClickerWeapon
	{
		public static readonly int EntangleCount = 5;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Nature, projsInheritItemElements: true);

			ClickEffect.Entangle = ClickerSystem.RegisterClickEffect(Mod, "Entangle", 10, new Color(155, 235, 80), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				int entangleType = ModContent.ProjectileType<FloralClickerPro>();

				//Despawn oldest vine
				
				List<Projectile> list = new();
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					Projectile proj = Main.projectile[i];

					if (proj.active && proj.type == entangleType && proj.owner == player.whoAmI)
					{
						list.Add(proj);
					}
				}

				list.Sort((p1, p2) => p2.timeLeft - p1.timeLeft);
				int limit = 4; //Actually 5
				if (list.Count >= limit)
				{
					for (int i = limit; i < list.Count; i++)
					{
						list[i].Kill();
					}
				}

				Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-5f, -2f), entangleType, damage / 3, 0f, player.whoAmI);	
			},
			descriptionArgs: new object[] { EntangleCount });
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 4f);
			SetColor(Item, new Color(175, 1240, 120));
			SetDust(Item, 157);
			AddEffect(Item, ClickEffect.Entangle);

			Item.damage = 40;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}
	}
}
