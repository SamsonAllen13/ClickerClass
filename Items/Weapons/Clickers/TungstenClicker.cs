using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class TungstenClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.45f);
			SetColor(Item, new Color(125, 175, 150));
			SetDust(Item, 83);
			AddEffect(Item, ClickEffect.DoubleClick2);

			Item.damage = 6;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 6750;
			Item.rare = 0;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.TungstenBar, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
