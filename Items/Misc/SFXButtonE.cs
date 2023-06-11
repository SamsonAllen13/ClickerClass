using Terraria.Audio;
using Terraria.ID;

namespace ClickerClass.Items.Misc
{
	public class SFXButtonE : SFXButtonBase
	{
		public static void PlaySound(int stack)
		{
			//Bell
			SoundEngine.PlaySound(SoundID.Item35
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
