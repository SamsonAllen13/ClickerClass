using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

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
			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 15;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255 - colorLoss, 255 - colorLoss, 150) * Projectile.Opacity;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.OnFire3, 300, false);
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
			if (Projectile.frame >= Main.projFrames[Projectile.type])
			{
				Projectile.frame = Main.projFrames[Projectile.type]; //Disappear after animation completed
			}
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.velocity = Projectile.oldVelocity / 3;
			if (Projectile.timeLeft > 20)
			{
				Projectile.timeLeft = 20;
			}

			return false;
		}
	}
}
