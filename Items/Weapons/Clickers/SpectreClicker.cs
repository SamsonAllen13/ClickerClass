using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SpectreClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			//Special tooltip set before this normally, but we use lang keys so it's handled automatically
			base.SetStaticDefaults();

			ClickEffect.PhaseReach = ClickerSystem.RegisterClickEffect(mod, "PhaseReach", null, null, 1, new Color(100, 255, 255), null);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 5f);
			SetColor(item, new Color(100, 255, 255));
			SetDust(item, 88);
			AddEffect(item, ClickEffect.PhaseReach);

			item.damage = 50;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 450000;
			item.rare = 8;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpectreBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
