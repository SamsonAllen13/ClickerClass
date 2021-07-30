using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SinisterClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Siphon = ClickerSystem.RegisterClickEffect(Mod, "Siphon", null, null, 10, new Color(100, 25, 25), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<SinisterClickerPro>(), (int)(damage * 0.50f), knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.2f);
			SetColor(Item, new Color(100, 25, 25));
			SetDust(Item, 5);
			AddEffect(Item, ClickEffect.Siphon);

			Item.damage = 10;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 18000;
			Item.rare = 1;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.CrimtaneBar, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
