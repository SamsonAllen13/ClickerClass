using Terraria;
using Terraria.Audio;

namespace ClickerClass.Items.Misc
{
	public class SFXButtonG : SFXButtonBase
	{
		public static void PlaySound(int stack)
		{
			//Duck quack + rare man-quack
			SoundStyle style = new SoundStyle(Main.rand.NextBool(200)
				? "Terraria/Sounds/Zombie_12"
				: "Terraria/Sounds/Zombie_11")
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
