using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CorruptClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Corrupt Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 3.25f);
			SetColor(item, new Color(125, 255, 0, 0));
			SetDust(item, 163);
			SetAmount(item, 8);
			SetEffect(item, "Cursed Eruption");


			item.damage = 31;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 105000;
			item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CursedFlame, 16);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
