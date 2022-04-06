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
		public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (!ClickerSystem.IsClickerProj(projectile))
			{
				return;
			}

			Player player = Main.player[projectile.owner];
			hitDirection = target.Center.X < player.Center.X ? -1 : 1;

			//Cannot use item/projectile.ArmorPenetration here because it's technically "infinite", and also conditional
			if (target.type != NPCID.DungeonGuardian && target.defense < 999)
			{
				int defenseIgnore = target.defense / 2;
				if (defenseIgnore <= 0) defenseIgnore = 0;
				damage += defenseIgnore;
			}
		}
	}
}
