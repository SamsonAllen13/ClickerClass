using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class PharaohsClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Arcane, projsInheritItemElements: true);
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Earth, projsInheritItemElements: true);

			ClickEffect.PharaohsCommand = ClickerSystem.RegisterClickEffect(Mod, "PharaohsCommand", 25, new Color(250, 220, 0), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, position.X, position.Y, 0f, 0f, ModContent.ProjectileType<PharaohsClickerPro>(), 0, 0f, player.whoAmI, 1f);
			},
			preHardMode: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.35f);
			SetColor(Item, new Color(250, 220, 20));
			SetDust(Item, 32);
			AddEffect(Item, ClickEffect.PharaohsCommand);

			Item.damage = 5;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Green;
		}
	}
}
