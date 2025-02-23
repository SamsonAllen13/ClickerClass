using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class RedHotClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Fire, projsInheritItemElements: true);

			ClickEffect.Inferno = ClickerSystem.RegisterClickEffect(Mod, "Inferno", 8, new Color(255, 125, 0), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<RedHotClickerPro>(), 0, knockBack, player.whoAmI);
			},
			preHardMode: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.6f);
			SetColor(Item, new Color(255, 125, 0));
			SetDust(Item, 174);
			AddEffect(Item, ClickEffect.Inferno);

			Item.damage = 12;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 0, 54, 0);
			Item.rare = ItemRarityID.Orange;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.HellstoneBar, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
