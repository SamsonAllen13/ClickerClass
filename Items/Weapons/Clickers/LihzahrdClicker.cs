using Microsoft.Xna.Framework;
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
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 4f);
			SetColor(item, new Color(200, 75, 0, 0));
			SetDust(item, 174);
			SetAmount(item, 10);
			SetEffect(item, "Solar Flare");


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
