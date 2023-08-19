using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class MagnetClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			//Special tooltip set before this normally, but we use lang keys so it's handled automatically
			base.SetStaticDefaults();

			ClickEffect.Yoink = ClickerSystem.RegisterClickEffect(Mod, "Yoink", 1, new Color(255, 180, 180), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				for (int j = 0; j < Main.maxItems; j++)
				{
					Item item = Main.item[j];
					if (!item.active || item.shimmerTime != 0f || item.noGrabDelay != 0 || (item.shimmered && item.velocity.LengthSquared() >= 0.2f * 0.2f) || Vector2.DistanceSquared(position, item.Center) >= 50 * 50 || !Collision.CanHit(position, 1, 1, item.Center, 1, 1))
					{
						continue;
					}

					item.Center = player.Center;
					//TODO sync?
				}
			},
			preHardMode: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 0.85f);
			SetColor(Item, new Color(255, 180, 180));
			SetDust(Item, 5);
			AddEffect(Item, ClickEffect.Yoink);

			Item.damage = 4;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 20, 0, 0);
			Item.rare = ItemRarityID.Orange;
		}
	}
}
