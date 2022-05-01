using ClickerClass.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Consumables
{
	public class InfluencePotion : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
				new Color(255, 163, 255),
				new Color(255, 58, 255),
				new Color(170, 0, 255)
			};
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.useTime = 17;
			Item.useAnimation = 17;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.useTurn = true;
			Item.value = Item.sellPrice(0, 0, 2, 0);
			Item.consumable = true;
			Item.maxStack = 30;
			Item.rare = 1;
			Item.UseSound = SoundID.Item3;
			Item.buffType = ModContent.BuffType<InfluenceBuff>();
			Item.buffTime = 18000;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.BottledWater, 1).AddIngredient(ItemID.Daybloom, 1).AddIngredient(ItemID.PinkGel, 1).AddTile(TileID.Bottles).Register();
		}
	}
}
