using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class UmbralClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Umbral Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.75f);
			SetColor(item, new Color(150, 100, 255, 0));
			SetDust(item, 27);
			SetAmount(item, 10);
			SetEffect(item, "Shadow Lash");


			item.damage = 20;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 200000;
			item.rare = 3;
		}

		/*
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<DarkClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SlickClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PointyClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<RedHotClicker>(), 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<SinisterClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SlickClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PointyClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<RedHotClicker>(), 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		*/
	}
}
