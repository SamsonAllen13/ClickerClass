using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ClickerClass.Tiles
{
	public class TrappedDungeonChest : TrappedChestBase
	{
		public override void SafeSetStaticDefaults()
		{
			AddMapEntry(new Color(174, 129, 92), ModContent.GetInstance<Items.Placeable.DungeonChest>().DisplayName);
		}
	}
}
