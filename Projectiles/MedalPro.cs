using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Projectiles
{
	public class MedalPro : ClickerProjectile
	{
		public float Rot = 0f;
		public float Alpha
		{
			get => Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}
		
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.aiStyle = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 150) * Alpha * Projectile.Opacity;
		}

		public Vector2 rotVec = new Vector2(0, 120);

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			
			if (player.whoAmI != Projectile.owner)
			{
				//Hide for everyone but the owner
				Projectile.alpha = 255;
			}

			Rot += 0.025f;
			Projectile.Center = player.Center + rotVec.RotatedBy(Rot + (Projectile.ai[0] * (6.28f / 2)));
			Projectile.gfxOffY = player.gfxOffY;

			if (Alpha > 0.1f)
			{
				Alpha -= 0.01f;
			}
			
			if (clickerPlayer.AccSMedal || clickerPlayer.AccFMedal)
			{
				Projectile.timeLeft = 10;
			}
			
			if (Projectile.ai[0] == 0)
			{
				if (clickerPlayer.AccSMedal)
				{
					Projectile.frame = 0;
					if (Alpha < 0.1f)
					{
						Alpha = 0.1f;
					}
				}
				else
				{
					Alpha = 0f;
				}
				return;
			}
			
			if (Projectile.ai[0] == 1)
			{
				if (clickerPlayer.AccFMedal)
				{
					Projectile.frame = 1;
					if (Alpha < 0.1f)
					{
						Alpha = 0.1f;
					}
				}
				else
				{
					Alpha = 0f;
				}
				return;
			}
		}
	}
}
