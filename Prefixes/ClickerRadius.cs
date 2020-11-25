using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Prefixes
{
	public class ClickerRadius : ModPrefix
	{
		public override PrefixCategory Category => PrefixCategory.Accessory;

		public override void SetDefaults()
		{
			DisplayName.SetDefault("Reaching");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Достигающий");
		}

		public override void ModifyValue(ref float valueMult) => valueMult *= 1.05f;
	}
}
