using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ClickerClass
{
	[Label("$Mods.ClickerClass.ClickerConfigClient.Label")]
	public class ClickerConfigClient : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		// Automatically set by tModLoader
		public static ClickerConfigClient Instance;

		[Header("$Mods.ClickerClass.ClickerConfigClient.Header.GeneralOptions")]

		[Label("$Mods.ClickerClass.ClickerConfigClient.ShowClassTags.Label")]
		[Tooltip("$Mods.ClickerClass.ClickerConfigClient.ShowClassTags.Tooltip")]
		[DefaultValue(true)]
		public bool ShowClassTags;
		
		[Label("$Mods.ClickerClass.ClickerConfigClient.ShowEffectSuggestion.Label")]
		[Tooltip("$Mods.ClickerClass.ClickerConfigClient.ShowEffectSuggestion.Tooltip")]
		[DefaultValue(true)]
		public bool ShowEffectSuggestion;

		[Label("$Mods.ClickerClass.ClickerConfigClient.ShowCustomCursors.Label")]
		[Tooltip("$Mods.ClickerClass.ClickerConfigClient.ShowCustomCursors.Tooltip")]
		[DefaultValue(true)]
		public bool ShowCustomCursors;

		[Label("$Mods.ClickerClass.ClickerConfigClient.ShowClickIndicator.Label")]
		[Tooltip("$Mods.ClickerClass.ClickerConfigClient.ShowClickIndicator.Tooltip")]
		[DefaultValue(false)]
		public bool ShowClickIndicator;

		[Label("$Mods.ClickerClass.ClickerConfigClient.ShowOthersClickIndicator.Label")]
		[Tooltip("$Mods.ClickerClass.ClickerConfigClient.ShowOthersClickIndicator.Tooltip")]
		[DefaultValue(true)]
		public bool ShowOthersClickIndicator;
	}
}
