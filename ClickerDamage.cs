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
				return 1f;
			}

			return base.GetBenefitFrom(damageClass);
		}
	}
}
