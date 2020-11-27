using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
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
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.45f);
			SetColor(item, new Color(255, 175, 0, 0));
			SetDust(item, 153);
			SetAmount(item, 1);
			SetEffect(item, "Sticky Honey");


			item.damage = 13;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 10000;
			item.rare = 3;
			item.shoot = ModContent.ProjectileType<HoneyGlazedClickerPro>();
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
