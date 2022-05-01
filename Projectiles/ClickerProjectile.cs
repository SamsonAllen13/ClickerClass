using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public abstract class ClickerProjectile : ModProjectile
	{
		/// <summary>
		/// Call this in the inherited class as base.SetStaticDefaults() at the start of SetStaticDefaults
		/// </summary>
		public override void SetStaticDefaults()
		{
			ClickerSystem.RegisterClickerProjectile(this);
		}

		/// <summary>
		/// Call this in the inherited class as base.SetDefaults() at the start of SetDefaults. You can change the default values after it
		/// </summary>
		public override void SetDefaults()
		{
			ClickerSystem.SetClickerProjectileDefaults(Projectile);
		}
	}
}
