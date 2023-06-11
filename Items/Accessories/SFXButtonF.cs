using Terraria.Audio;
using Terraria.ID;

namespace ClickerClass.Items.Accessories
{
	public class SFXButtonF : SFXButtonBase
	{
		public static void PlaySound(int stack)
		{
			//Windy Balloon pop
			SoundEngine.PlaySound(SoundID.NPCDeath63
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
