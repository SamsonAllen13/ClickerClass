using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class MythrilClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Embrittle = ClickerSystem.RegisterClickEffect(Mod, "Embrittle", null, null, 10, new Color(125, 225, 125), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<MythrilClickerPro>(), 0, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.95f);
			SetColor(Item, new Color(125, 225, 125));
			SetDust(Item, 49);
			AddEffect(Item, ClickEffect.Embrittle);

			Item.damage = 22;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 103500;
			Item.rare = 4;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.MythrilBar, 8).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
