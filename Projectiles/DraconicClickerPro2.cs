using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ClickerClass.Projectiles
{
	public class DraconicClickerPro2 : ClickerProjectile
	{
		public int colorLoss = 0;
		
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 7;
		}
		
		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 98;
			Projectile.height = 98;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 180;
			Projectile.friendly = true;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255 - colorLoss, 255 - colorLoss, 150) * Projectile.Opacity;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Oiled, 300, false);
			target.AddBuff(BuffID.OnFire, 300, false);
		}
		
		public override void AI()
		{
			Projectile.alpha += 5;
			Projectile.rotation -= 0.1f;
			colorLoss += 5;
			
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 4)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= 7)
			{
				Projectile.frame = 7;
			}
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.velocity = Projectile.oldVelocity;
			if (Projectile.timeLeft > 20)
			{
				Projectile.timeLeft = 20;
			}

			return false;
		}
	}
}
