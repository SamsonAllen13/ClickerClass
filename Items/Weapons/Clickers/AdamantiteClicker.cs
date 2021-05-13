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

			ClickEffect.TrueStrike = ClickerSystem.RegisterClickEffect(mod, "TrueStrike", null, null, 10, new Color(255, 25, 25), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AdamantiteClickerPro>(), damage, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 3.15f);
			SetColor(item, new Color(255, 25, 25));
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
