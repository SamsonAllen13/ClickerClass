using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class AdamantiteClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.TrueStrike = ClickerSystem.RegisterClickEffect(Mod, "TrueStrike", null, null, 10, new Color(255, 25, 25), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AdamantiteClickerPro>(), damage, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.15f);
			SetColor(Item, new Color(255, 25, 25));
			SetDust(Item, 50);
			AddEffect(Item, ClickEffect.TrueStrike);

			Item.damage = 30;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 138000;
			Item.rare = 4;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.AdamantiteBar, 8).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
