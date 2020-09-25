using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class ClickerGlobalProjectile : GlobalProjectile
	{
		public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (!(projectile.modProjectile is ClickerProjectile clickerProjectile && clickerProjectile.isClickerProj))
			{
				return;
			}

			Player player = Main.player[projectile.owner];

			// Vanilla crit chance calculations. Crit chance of the currently held weapon matters, regardless of the damage type of the weapon.
			int critChance = player.HeldItem.crit;
			ItemLoader.GetWeaponCrit(player.HeldItem, player, ref critChance);
			PlayerHooks.GetWeaponCrit(player, player.HeldItem, ref critChance);
			crit = critChance >= 100 || Main.rand.Next(1, 101) <= critChance;
		}
	}
}