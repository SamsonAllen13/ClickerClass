using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Tiles.Mice
{
	public class MiceChest : ChestTileBase
	{
		public override int PlacedItemType => ModContent.ItemType<Items.Placeable.Mice.MiceChest>();

		public override Color MapColor => new Color(172, 189, 246);

		public override void SafeSetStaticDefaults()
		{
			Main.tileOreFinderPriority[Type] = 500;
		}
	}
}
