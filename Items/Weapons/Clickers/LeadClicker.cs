using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class LeadClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.3f);
			SetColor(Item, new Color(75, 75, 125));
			SetDust(Item, 82);
			AddEffect(Item, ClickEffect.DoubleClick);

			item.damage = 3;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 2700;
			item.rare = 0;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.LeadBar, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
