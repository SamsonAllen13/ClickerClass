using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Utilities
{
	public static class LangHelper
	{
		/// <summary>
		/// prefixes the modname for the key
		/// </summary>
		/// <returns>Text associated with this key</returns>
		public static string GetTextByMod(Mod mod, string key, params object[] args)
		{
			return Language.GetTextValue($"Mods.{mod.Name}.{key}", args);
		}

		/// <summary>
		/// prefixes the modname for the key
		/// </summary>
		/// <returns>LocalizedText associated with this key</returns>
		public static LocalizedText GetLocalizationByMod(Mod mod, string key)
		{
			return Language.GetText($"Mods.{mod.Name}.{key}");
		}

		/// <summary>
		/// Defaults to Mods.ClickerClass. as the prefix for the key
		/// </summary>
		/// <returns>Text associated with this key</returns>
		internal static string GetText(string key, params object[] args)
		{
			return GetTextByMod(ClickerClass.mod, key, args);
		}

		/// <summary>
		/// Defaults to Mods.ClickerClass. as the prefix for the key
		/// </summary>
		/// <returns>LocalizedText associated with this key</returns>
		internal static LocalizedText GetLocalization(string key)
		{
			return GetLocalizationByMod(ClickerClass.mod, key);
		}
	}
}
