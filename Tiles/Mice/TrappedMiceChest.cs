using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ClickerClass.Tiles.Mice
{
	public class TrappedMiceChest : TrappedChestBase
	{
		public override void SafeSetStaticDefaults()
		{
			AddMapEntry(new Color(172, 189, 246), ModContent.GetInstance<Items.Placeable.Mice.MiceChest>().DisplayName);
		}
	}
}
