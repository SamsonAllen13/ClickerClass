using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace ClickerClass.Projectiles
{
	public class CookiePro : ClickerProjectile
	{
		public Vector2 location = Vector2.Zero;

		public int Frame
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		//Should not be synced, as location isn't synced either
		public bool LockedLocation
		{
			get => Projectile.localAI[1] == 1f;
			set => Projectile.localAI[1] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}

		public override void OnSpawn(IEntitySource source)
		{
			if (source is not EntitySource_ItemUse itemUse || itemUse.Entity is not Player player)
			{
				return;
			}

			if (player.GetModPlayer<ClickerPlayer>().accCookie2 && Main.rand.NextFloat() <= 0.1f)
			{
				Frame = 1;
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			float time = Projectile.timeLeft;
			if (Projectile.timeLeft > 150)
			{
				time = 300 - Projectile.timeLeft;
			}
			return new Color(255, 255, 255, 200) * 0.005f * time * Projectile.Opacity;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			Projectile.frame = Frame;
			Projectile.rotation -= 0.01f;

			if (player.whoAmI != Projectile.owner)
			{
				//Hide for everyone but the owner
				Projectile.alpha = 255;
			}

			if (!LockedLocation)
			{
				location = player.Center - Projectile.Center;
				LockedLocation = true;
			}

			Projectile.Center = player.Center - location;
			Projectile.gfxOffY = player.gfxOffY;
		}
	}
}
