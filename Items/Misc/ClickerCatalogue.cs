using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ClickerClass.Items.Placeable;

namespace ClickerClass.Items.Misc
{
	public class ClickerCatalogue : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.maxStack = 1;
			Item.value = 00;
			Item.rare = ItemRarityID.Orange;
		}
	}
}
