using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SandstormClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Earth, projsInheritItemElements: true);
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Wind, projsInheritItemElements: true);

			ClickEffect.DustDevil = ClickerSystem.RegisterClickEffect(Mod, "DustDevil", 10, new Color(220, 185, 120), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<SandstormClickerPro>(), damage, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.1f);
			SetColor(Item, new Color(225, 170, 125));
			SetDust(Item, 216);
			AddEffect(Item, ClickEffect.DustDevil);

			Item.damage = 26;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(0, 2, 10, 0);
			Item.rare = ItemRarityID.Pink;
		}
	}
}
