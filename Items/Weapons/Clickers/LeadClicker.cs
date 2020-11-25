using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class LeadClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Lead Clicker");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Свинцовый курсор");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 1.3f);
			SetColor(item, new Color(75, 75, 125, 0));
			SetDust(item, 82);
			SetAmount(item, 10);
			SetEffect(item, "Double Click");


			item.damage = 5;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 2700;
			item.rare = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LeadBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
