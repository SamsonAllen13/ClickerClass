using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class AdamantiteClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Adamantite Clicker");

			ClickEffect.TrueStrike = ClickerSystem.RegisterClickEffect(mod, "TrueStrike", "True Strike", "Deals double damage and always inflicts a critical hit", 10, new Color(255, 25, 25, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 71);
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<AdamantiteClickerPro>(), damage, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			SetRadius(item, 3.15f);
			SetColor(item, new Color(255, 25, 25, 0));
			SetDust(item, 50);
			AddEffect(item, ClickEffect.TrueStrike);

			item.damage = 32;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 138000;
			item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.AdamantiteBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
