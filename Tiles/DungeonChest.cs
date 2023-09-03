using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace ClickerClass.Tiles
{
	public class DungeonChest : ChestTileBase
	{
		public override int PlacedItemType => ModContent.ItemType<Items.Placeable.DungeonChest>();

		public override int KeyItemType => ModContent.ItemType<Items.Misc.DungeonKey>();

		public override void SafeSetStaticDefaults()
		{
			Main.tileShine2[Type] = true;
			Main.tileShine[Type] = 1200;
			Main.tileOreFinderPriority[Type] = 500;

			DustType = 275;
		}

		public override bool UnlockChest(int i, int j, ref short frameXAdjustment, ref int dustType, ref bool manual)
		{
			if (!NPC.downedPlantBoss)
			{
				return false;
			}
			AchievementsHelper.NotifyProgressionEvent(20);
			dustType = DustType;
			return true;
		}
	}
}
