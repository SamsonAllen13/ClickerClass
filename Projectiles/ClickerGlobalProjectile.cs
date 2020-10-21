using Terraria;
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

			Item heldItem = player.HeldItem;

			// Prevent crash when held item is null/none
			if (heldItem == null) return;

			// Vanilla crit chance calculations. Crit chance of the currently held weapon matters, regardless of the damage type of the weapon.
			int critChance = heldItem.crit;
			ItemLoader.GetWeaponCrit(heldItem, player, ref critChance);
			PlayerHooks.GetWeaponCrit(player, heldItem, ref critChance);
			if (!crit)
			{
				crit = critChance >= 100 || Main.rand.Next(1, 101) <= critChance;
			}

			int defenseIgnore = target.defense / 2;
			if (defenseIgnore <= 0) defenseIgnore = 0;
			damage += defenseIgnore;
			hitDirection = target.Center.X < player.Center.X ? -1 : 1;
		}
	}
}