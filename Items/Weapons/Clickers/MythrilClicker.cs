using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class MythrilClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mythril Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Мифриловый курсор");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.95f);
			SetColor(item, new Color(125, 225, 125, 0));
			SetDust(item, 49);
			SetAmount(item, 10);
			SetEffect(item, "Embrittle");


			item.damage = 25;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 103500;
			item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MythrilBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
