using ClickerClass.Dusts;
using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class AstralClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Spiral = ClickerSystem.RegisterClickEffect(mod, "Spiral", null, null, 15, new Color(150, 150, 225), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AstralClickerPro>(), (int)(damage * 3f), 0f, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 6f);
			SetColor(item, new Color(150, 150, 225));
			SetDust(item, ModContent.DustType<MiceDust>());
			AddEffect(item, ClickEffect.Spiral);

			item.damage = 82;
			item.knockBack = 1f;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 10;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<MiceFragment>(), 18);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
