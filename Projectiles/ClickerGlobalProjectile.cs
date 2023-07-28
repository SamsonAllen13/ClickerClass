using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	/// <summary>
	/// Handles hitDirection and defense ignore for all registered clicker projectiles safely
	/// </summary>
	public class ClickerGlobalProjectile : GlobalProjectile
	{
		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
		{
			return ClickerSystem.IsClickerProj(entity.type);
		}

		public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
		{
			Player player = Main.player[projectile.owner];
			modifiers.HitDirectionOverride = target.Center.X < player.Center.X ? -1 : 1;

			if (!target.SuperArmor)
			{
				modifiers.ScalingArmorPenetration += 1f;
			}
		}
	}
}
