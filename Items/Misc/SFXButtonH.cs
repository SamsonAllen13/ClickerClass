using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ClickerClass.Items.Misc
{
	public class SFXButtonH : SFXButtonBase
	{
		public static void PlaySound(int stack)
		{
			//Research crusher + rare research complete jingle
			SoundStyle style = (Main.rand.NextBool(200) ? SoundID.Research : SoundID.ResearchComplete)
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
