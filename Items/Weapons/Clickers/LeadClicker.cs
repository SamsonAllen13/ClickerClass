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

			Item.damage = 5;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 2700;
			Item.rare = 0;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.LeadBar, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
