using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class EclipticClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Fire, projsInheritItemElements: true);
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Shadow, projsInheritItemElements: true);
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Celestial, projsInheritItemElements: true);

			ClickEffect.Totality = ClickerSystem.RegisterClickEffect(Mod, "Totality", 15, new Color(255, 200, 100), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<TotalityClickerPro>(), damage, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.5f);
			SetColor(Item, new Color(255, 200, 100));
			SetDust(Item, 264);
			AddEffect(Item, ClickEffect.Totality);

			Item.damage = 28;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 3, 30, 0);
			Item.rare = ItemRarityID.LightPurple;
		}
	}
}
