using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ClickerClass.Items.Placeable;

namespace ClickerClass.Items.Misc
{
	public class DungeonKey : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.UsesCursedByPlanteraTooltip[Item.type] = true;
			ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<DungeonChest>();
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 30;
			Item.maxStack = 9999;
			Item.value = 00;
			Item.rare = ItemRarityID.Yellow;
		}
	}
}
