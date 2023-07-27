using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ClickerClass.Items.Weapons.Clickers;

namespace ClickerClass.Tiles
{
	public class ClickerGlobalTile : GlobalTile
	{
		public override void Drop(int i, int j, int type)
		{
			if (type == TileID.Pigronata && Main.rand.NextBool(50))
			{
				Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<ConfettiClicker>());
			}

			if (type == TileID.Heart && Main.rand.NextBool(50))
			{
				Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<HeartyClicker>());
			}
		}
	}
}
