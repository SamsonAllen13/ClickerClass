using ClickerClass.Dusts;
using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class AstralClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Spiral = ClickerSystem.RegisterClickEffect(Mod, "Spiral", null, null, 15, new Color(150, 150, 225), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AstralClickerPro>(), (int)(damage * 3f), 0f, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6f);
			SetColor(Item, new Color(150, 150, 225));
			SetDust(Item, ModContent.DustType<MiceDust>());
			AddEffect(Item, ClickEffect.Spiral);

			Item.damage = 82;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = 10;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceFragment>(), 18).AddTile(TileID.LunarCraftingStation).Register();
		}
	}
}
