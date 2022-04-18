using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Projectiles
{
	public abstract class SMedalProBase : ClickerProjectile
	{
		public const int TotalCount = 3;

		public int Mode
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public float Alpha
		{
			get => Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public float Rot
		{
			get => Projectile.localAI[0];
			set => Projectile.localAI[0] = value;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
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
			
			if (clickerPlayer.AccSMedal)
			{
				Projectile.timeLeft = 10;
			}

			Rot = clickerPlayer.accMedalRot;
			Projectile.Center = player.Center + rotVec.RotatedBy(Rot + (Mode * (6.28f / TotalCount)));
			Projectile.gfxOffY = player.gfxOffY;

			if (Alpha > 0.1f)
			{
				Alpha -= 0.01f;
			}

			if (Alpha < 0.1f)
			{
				Alpha = 0.1f;
			}
		}
	}
}
