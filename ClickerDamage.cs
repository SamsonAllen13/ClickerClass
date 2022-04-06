using Terraria.ModLoader;

namespace ClickerClass
{
	public class ClickerDamage : DamageClass
	{
		public override void SetStaticDefaults()
		{
			ClassName.SetDefault("click damage");
		}

		//TODO open problem: all weapons in the game seem to have 0% crit
	}
}
