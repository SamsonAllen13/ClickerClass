using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class ClickerGlobalProjectile : GlobalProjectile
	{
		public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (!ClickerSystem.IsClickerProj(projectile))
			{
				return;
			}

			Player player = Main.player[projectile.owner];

			//No manual clicker crit calculations anymore due to DamageClass

			//This behavior is special to clicker projectiles, not just click damage
			int defenseIgnore = target.defense / 2;
			if (defenseIgnore <= 0) defenseIgnore = 0;
			damage += defenseIgnore;
			hitDirection = target.Center.X < player.Center.X ? -1 : 1;
		}
	}
}
