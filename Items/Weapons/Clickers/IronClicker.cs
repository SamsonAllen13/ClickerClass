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
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.25f);
			SetColor(Item, new Color(150, 125, 125));
			SetDust(Item, 8);
			AddEffect(Item, ClickEffect.DoubleClick);

			Item.damage = 3;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 0.5f;
			Item.value = 1800;
			Item.rare = 0;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.IronBar, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
