using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class BlizzardClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Flurry = ClickerSystem.RegisterClickEffect(Mod, "Flurry", null, null, 15, new Color(150, 220, 255), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 30), Vector2.Zero, ModContent.ProjectileType<BlizzardClickerPro>(), damage, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3f);
			SetColor(Item, new Color(150, 220, 255));
			SetDust(Item, 92);
			AddEffect(Item, ClickEffect.Flurry);

			Item.damage = 26;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(0, 2, 10, 0);
			Item.rare = ItemRarityID.Pink;
		}
	}
}
