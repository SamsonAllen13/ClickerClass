using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class WoodenClicker : ClickerWeapon
	{
		public override void Load()
		{
			On_Item.CanShimmer += On_Item_CanShimmer;
		}

		private static bool On_Item_CanShimmer(On_Item.orig_CanShimmer orig, Item self)
		{
			bool ret = orig(self);

			//Workaround for lack of conditions on item shimmer
			if (self.type == ModContent.ItemType<WoodenClicker>() && !NPC.downedMoonlord)
			{
				return false;
			}

			return ret;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<QuintessenceClicker>();
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 0.85f);
			SetColor(Item, new Color(125, 100, 75));
			SetDust(Item, 0);

			Item.damage = 2;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 0.5f;
			Item.value = Item.sellPrice(0, 0, 0, 5);
			Item.rare = ItemRarityID.White;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.Wood, 10).AddTile(TileID.WorkBenches).Register();
		}
	}
}
