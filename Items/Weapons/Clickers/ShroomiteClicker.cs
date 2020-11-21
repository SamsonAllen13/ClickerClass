using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class ShroomiteClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Shroomite Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 6f);
			SetColor(item, new Color(150, 150, 255, 0));
			SetDust(item, 113);
			SetAmount(item, 20);
			SetEffect(item, "Auto Click");


			item.damage = 68;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 450000;
			item.rare = 8;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ShroomiteBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
