using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class GoldClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.6f);
			SetColor(Item, new Color(255, 200, 25));
			SetDust(Item, 10);
			AddEffect(Item, ClickEffect.DoubleClick2);

			Item.damage = 5;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 0.5f;
			Item.value = Item.sellPrice(0, 0, 18, 0);
			Item.rare = ItemRarityID.White;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.GoldBar, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
