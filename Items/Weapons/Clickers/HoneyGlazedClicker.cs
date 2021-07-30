using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class HoneyGlazedClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.StickyHoney = ClickerSystem.RegisterClickEffect(Mod, "StickyHoney", null, null, 1, new Color(255, 175, 0), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<HoneyGlazedClickerPro>(), 0, 0f, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.45f);
			SetColor(Item, new Color(255, 175, 0));
			SetDust(Item, 153);
			AddEffect(Item, ClickEffect.StickyHoney);

			Item.damage = 13;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 10000;
			Item.rare = 3;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.BeeWax, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
