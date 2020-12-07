using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class LihzahrdClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Lihzahrd Clicker");

			ClickEffect.SolarFlare = ClickerSystem.RegisterClickEffect(mod, "SolarFlare", "Solar Flare", "Spawns a lingering sun projectile that damages and inflicts the Oiled and On Fire! debuffs", 10, new Color(200, 75, 0, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 68);
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<LihzarhdClickerPro>(), (int)(damage * 0.5f), 0f, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 4f);
			SetColor(item, new Color(200, 75, 0, 0));
			SetDust(item, 174);
			AddEffect(item, ClickEffect.SolarFlare);

			item.damage = 66;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 300000;
			item.rare = 7;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarTabletFragment, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
