using Terraria.ModLoader;

namespace ClickerClass.Prefixes
{
	public class ClickerRadius : ModPrefix
	{
		public override PrefixCategory Category => PrefixCategory.Accessory;

		public override void ModifyValue(ref float valueMult) => valueMult *= 1.05f;
	}
}
