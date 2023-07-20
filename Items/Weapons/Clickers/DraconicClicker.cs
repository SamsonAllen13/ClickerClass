using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class DraconicClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.WyvernsRoar = ClickerSystem.RegisterClickEffect(Mod, "WyvernsRoar", 10, new Color(175, 55, 135), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				int offSet = player.direction * 350;
				float direction = player.direction * 3f;

				Projectile.NewProjectile(source, new Vector2(position.X - offSet, position.Y - 225), new Vector2(direction, 0f), ModContent.ProjectileType<DraconicClickerPro>(), damage * 2, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 5.65f);
			SetColor(Item, new Color(160, 25, 115));
			SetDust(Item, 174);
			AddEffect(Item, ClickEffect.WyvernsRoar);

			Item.damage = 66;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}
	}
}
