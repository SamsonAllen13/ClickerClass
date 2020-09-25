using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ClickerClass.Items;

namespace ClickerClass.Projectiles
{
	public class ClickDamage : ClickerProjectile
	{
		public override void SetDefaults()
		{
			isClickerProj = true;
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = -1;
			projectile.alpha = 255;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 10;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
		}
		
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[projectile.owner];
			damage = (int)(damage + (target.defense / 2));
			hitDirection = target.Center.X < player.Center.X ? -1 : 1;
		}
		
		public override void Kill(int timeLeft)
		{
			Player player = Main.player[projectile.owner];
			
			int dustColor = 0;
			if (player.HeldItem.modItem is ClickerItem clickerItem && clickerItem.isClicker)
			{
				dustColor = clickerItem.clickerDustColor;
			}
			
			for (int k = 0; k < 5; k++)
			{
				Dust dust = Dust.NewDustDirect(projectile.Center, 10, 10, dustColor, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 75, default, 1f);
				dust.noGravity = true;
			}
		}
	}
}