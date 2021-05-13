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
	}
}
