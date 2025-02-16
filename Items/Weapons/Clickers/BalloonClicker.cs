using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class BalloonClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Wind, projsInheritItemElements: true);

			ClickEffect.BalloonDefense = ClickerSystem.RegisterClickEffect(Mod, "BalloonDefense", 20, new Color(200, 125, 125), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, position.X, position.Y - 500, 0f, 15f, ModContent.ProjectileType<BalloonClickerPro>(), damage, 0f, player.whoAmI);
			},
			preHardMode: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.15f);
			SetColor(Item, new Color(180, 75, 75));
			SetDust(Item, 36);
			AddEffect(Item, ClickEffect.BalloonDefense);

			Item.damage = 4;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 0, 10, 0);
			Item.rare = ItemRarityID.Blue;
		}
	}
}
