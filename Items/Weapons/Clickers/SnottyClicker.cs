using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SnottyClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Poison, projsInheritItemElements: true);

			ClickEffect.OgreGold = ClickerSystem.RegisterClickEffect(Mod, "OgreGold", 1, new Color(180, 215, 150), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<SnottyClickerPro>(), 0, 0f, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.9f);
			SetColor(Item, new Color(180, 215, 150));
			SetDust(Item, 188);
			AddEffect(Item, ClickEffect.OgreGold);

			Item.damage = 42;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.LightPurple;
		}
	}
}
