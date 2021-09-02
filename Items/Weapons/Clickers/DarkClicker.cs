using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class DarkClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.DarkBurst = ClickerSystem.RegisterClickEffect(Mod, "DarkBurst", null, null, 8, new Color(100, 50, 200), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<DarkClickerPro>(), damage, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.1f);
			SetColor(Item, new Color(100, 50, 200));
			SetDust(Item, 14);
			AddEffect(Item, ClickEffect.DarkBurst);

			Item.damage = 6;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 18000;
			Item.rare = 1;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.DemoniteBar, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
