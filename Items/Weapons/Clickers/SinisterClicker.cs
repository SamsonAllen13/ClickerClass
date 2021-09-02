using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SinisterClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Siphon = ClickerSystem.RegisterClickEffect(mod, "Siphon", null, null, 10, new Color(100, 25, 25), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<SinisterClickerPro>(), (int)(damage * 0.50f), knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.2f);
			SetColor(item, new Color(100, 25, 25));
			SetDust(item, 5);
			AddEffect(item, ClickEffect.Siphon);

			item.damage = 6;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 18000;
			item.rare = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CrimtaneBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
