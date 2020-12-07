using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class HoneyGlazedClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Honey Glazed Clicker");

			ClickEffect.StickyHoney = ClickerSystem.RegisterClickEffect(mod, "StickyHoney", "Sticky Honey", "Covers enemies under cursor in sticky honey, slowing them", 1, new Color(255, 175, 0, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<HoneyGlazedClickerPro>(), 0, 0f, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.45f);
			SetColor(item, new Color(255, 175, 0, 0));
			SetDust(item, 153);
			AddEffect(item, ClickEffect.StickyHoney);

			item.damage = 13;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 10000;
			item.rare = 3;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BeeWax, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
