using Terraria;
using Terraria.Audio;

namespace ClickerClass.Items.Accessories
{
	public class SFXButtonC : SFXButtonBase
	{
		public static void PlaySound(int stack)
		{
			//Ogre OUAGH
			SoundStyle style = new SoundStyle(Main.rand.NextBool()
				? "Terraria/Sounds/Custom/dd2_ogre_attack_2"
				: "Terraria/Sounds/Custom/dd2_ogre_hurt_1")
				.WithVolumeScale(.4f * stack) with
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
