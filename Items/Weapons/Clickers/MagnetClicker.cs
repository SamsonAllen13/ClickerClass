using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class MagnetClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Yoink = ClickerSystem.RegisterClickEffect(Mod, "Yoink", null, null, 1, new Color(255, 180, 180), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				for (int j = 0; j < Main.maxItems; j++)
				{
					Item item = Main.item[j];
					if (item.active && item.noGrabDelay == 0 && !item.beingGrabbed && Vector2.DistanceSquared(position, item.Center) < 50 * 50 && Collision.CanHit(position, 1, 1, item.Center, 1, 1))
					{
						item.Center = player.Center;
					}
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2f);
			SetColor(Item, new Color(255, 180, 180));
			SetDust(Item, 5);
			AddEffect(Item, ClickEffect.Yoink);

			Item.damage = 4;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 1000000;
			Item.rare = 3;
		}
	}
}
