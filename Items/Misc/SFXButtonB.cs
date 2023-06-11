using Terraria.Audio;
using Terraria.ID;

namespace ClickerClass.Items.Misc
{
	public class SFXButtonB : SFXButtonBase
	{
		public static void PlaySound(int stack)
		{
			//Insect chirp
			SoundEngine.PlaySound(SoundID.NPCHit29
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
