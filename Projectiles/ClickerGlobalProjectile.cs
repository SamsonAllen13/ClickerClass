using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	/// <summary>
	/// Handles hitDirection and defense ignore for all registered clicker projectiles safely
	/// </summary>
	public class ClickerGlobalProjectile : GlobalProjectile
	{
		public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
		{
			if (!ClickerSystem.IsClickerProj(projectile))
			{
				return;
			}

			Player player = Main.player[projectile.owner];
			modifiers.HitDirectionOverride = target.Center.X < player.Center.X ? -1 : 1;

			if (target.type != NPCID.DungeonGuardian && target.defense < 999)
			{
				modifiers.ScalingArmorPenetration += 1f;
			}
		}
	}
}
