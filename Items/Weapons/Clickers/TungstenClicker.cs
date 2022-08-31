using Microsoft.Xna.Framework;
using Terraria;
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

			Item.damage = 4;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 0, 13, 50);
			Item.rare = ItemRarityID.White;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.TungstenBar, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
