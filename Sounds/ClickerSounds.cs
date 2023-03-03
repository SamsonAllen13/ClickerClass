using Terraria.Audio;

namespace ClickerClass.Sounds
{
	/// <summary>
	/// Contains all custom sounds as "prefabs"
	/// </summary>
	public static partial class ClickerSounds
	{
		//How to:
		/*
		 * - PitchRange and PitchVariance are mutually exclusive
		 * 
		 * - "MaxInstances = 1, SoundLimitBehavior = SoundLimitBehavior.IgnoreNew," replicates "soundInstance.State == SoundState.Playing -> return null" OR not having "sound.CreateInstance()"
		 */

		// Take note of the regions, and for consistency, order new sounds alphabetically based on the region/category

		public const string PathPfx = "ClickerClass/Sounds/";
		public const string Custom = "Custom/";

		public static SoundStyle Trumpet = new(PathPfx + Custom + "Trumpet")
		{
			Volume = 0.8f,
		};
	}
}
