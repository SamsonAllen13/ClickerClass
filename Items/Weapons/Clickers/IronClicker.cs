using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class IronClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Iron Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 1.25f);
			SetColor(item, new Color(150, 125, 125, 0));
			SetDust(item, 8);
			SetAmount(item, 10);
			SetEffect(item, "Double Click");


			item.damage = 5;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 1800;
			item.rare = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
