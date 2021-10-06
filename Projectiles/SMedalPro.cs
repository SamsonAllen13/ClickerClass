using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class SMedalPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 150) * Projectile.ai[1];
		}

		public Vector2 rotVec = new Vector2(0, 120);
		public float rot = 0f;

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			
			if (clickerPlayer.accSMedal)
			{
				Projectile.timeLeft = 10;
			}

			rot += 0.025f;
			Projectile.Center = player.Center + rotVec.RotatedBy(rot + (Projectile.ai[0] * (6.28f / 1)));
			if (Projectile.ai[1] > 0.1f)
			{
				Projectile.ai[1] -= 0.01f;
			}
		}
	}
}
