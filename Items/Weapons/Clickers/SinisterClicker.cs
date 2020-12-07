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
			DisplayName.SetDefault("Sinister Clicker");

			ClickEffect.Siphon = ClickerSystem.RegisterClickEffect(mod, "Siphon", "Siphon", "Deals a small amount of damage and restores the player's health by 5", 10, new Color(100, 25, 25, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 112);
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<SinisterClickerPro>(), (int)(damage * 0.50f), knockBack, player.whoAmI);
				for (int i = 0; i < 15; i++)
				{
					int num6 = Dust.NewDust(Main.MouseWorld, 20, 20, 5, 0f, 0f, 75, default(Color), 1.5f);
					Main.dust[num6].noGravity = true;
					Main.dust[num6].velocity *= 0.75f;
					int num7 = Main.rand.Next(-50, 51);
					int num8 = Main.rand.Next(-50, 51);
					Dust dust = Main.dust[num6];
					dust.position.X += num7;
					Dust dust2 = Main.dust[num6];
					dust2.position.Y += num8;
					Main.dust[num6].velocity.X = -num7 * 0.075f;
					Main.dust[num6].velocity.Y = -num8 * 0.075f;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.2f);
			SetColor(item, new Color(100, 25, 25, 0));
			SetDust(item, 5);
			AddEffect(item, ClickEffect.Siphon);

			item.damage = 10;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
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
