using ClickerClass.Sounds;
using Terraria.Audio;

namespace ClickerClass.Items.Misc
{
	public class SFXButtonA : SFXButtonBase
	{
		public static void PlaySound(int stack)
		{
			//Trumpet doot
			SoundStyle style = ClickerSounds.Trumpet
				.WithVolumeScale(.5f * stack) with
			{
				PitchVariance = .5f,
			};
			SoundEngine.PlaySound(style);
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickerSystem.RegisterSFXButton(this, PlaySound);
		}
	}
}
