using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Terraria.ModLoader.Config;

namespace ClickerClass
{
	public class ClickerConfigClient : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		// Automatically set by tModLoader
		public static ClickerConfigClient Instance;

		[Header("DisplayOptions")]

		[DefaultValue(true)]
		public bool ShowClassTags;

		[DefaultValue(true)]
		public bool ShowEffectSuggestion;

		[DefaultValue(true)]
		public bool ShowCustomCursors;

		[DefaultValue(false)]
		public bool ShowClickIndicator;

		[DefaultValue(true)]
		public bool ShowOthersClickIndicator;

		[Header("GameplayOptions")]

		[BackgroundColor(255, 175, 100)]
		[DefaultValue(true)]
		public bool ToggleAutoreuseLimiter;

		public const int ToggleAutoreuseLimiterValue_Min = 1;
		public const int ToggleAutoreuseLimiterValue_Max = 30;
		[BackgroundColor(255, 175, 100)]
		[Range(ToggleAutoreuseLimiterValue_Min, ToggleAutoreuseLimiterValue_Max), Increment(1), DefaultValue(15)]
		[Slider]
		public int ToggleAutoreuseLimiterValue;
		
		[BackgroundColor(255, 175, 100)]
		[DefaultValue(false)]
		public bool ToggleAutoreuseLimiterAccessibility;

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
			Clamp(ref ToggleAutoreuseLimiterValue, ToggleAutoreuseLimiterValue_Min, ToggleAutoreuseLimiterValue_Max);
		}

		public static void Clamp(ref int value, int min, int max)
		{
			value = value < min ? min : (value > max ? max : value);
		}
	}
}
