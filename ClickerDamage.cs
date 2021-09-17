using Terraria.ModLoader;

namespace ClickerClass
{
	public class ClickerDamage : DamageClass
	{
		public override void SetStaticDefaults()
		{
			ClassName.SetDefault("click damage");
		}

		protected override float GetBenefitFrom(DamageClass damageClass)
		{
			if (damageClass == Generic)
			{
				//Affected by allDamage/Crit, applies base level crit = 4 to player
				return 1f;
			}

			return base.GetBenefitFrom(damageClass);
		}
	}
}
