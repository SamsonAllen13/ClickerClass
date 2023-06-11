using Terraria.Audio;
using Terraria.ID;

namespace ClickerClass.Items.Misc
{
	public class SFXButtonD : SFXButtonBase
	{
		public static void PlaySound(int stack)
		{
			//Fartè
			SoundEngine.PlaySound(SoundID.Item16
				.WithVolumeScale(.5f * stack) with
			{
				PitchVariance = .5f
			});
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickerSystem.RegisterSFXButton(this, PlaySound);
		}
	}
}
