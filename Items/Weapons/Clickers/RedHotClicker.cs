using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class RedHotClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Red Hot Clicker");

			ClickEffect.Inferno = ClickerSystem.RegisterClickEffect(mod, "Inferno", "Inferno", "Creates an explosion, dealing damage and inflicting the Oiled and On Fire! debuffs", 8, new Color(255, 125, 0, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 74);
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<RedHotClickerPro>(), 0, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.6f);
			SetColor(item, new Color(255, 125, 0, 0));
			SetDust(item, 174);
			AddEffect(item, ClickEffect.Inferno);

			item.damage = 17;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 27000;
			item.rare = 3;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HellstoneBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
