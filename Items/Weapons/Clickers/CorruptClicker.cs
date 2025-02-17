using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CorruptClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Shadow, projsInheritItemElements: true);

			ClickEffect.CursedEruption = ClickerSystem.RegisterClickEffect(Mod, "CursedEruption", 8, new Color(125, 255, 0), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<CorruptClickerPro>(), damage, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.25f);
			SetColor(Item, new Color(125, 255, 0));
			SetDust(Item, 163);
			AddEffect(Item, ClickEffect.CursedEruption);

			Item.damage = 24;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(0, 2, 10, 0);
			Item.rare = ItemRarityID.LightRed;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.CursedFlame, 16).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
