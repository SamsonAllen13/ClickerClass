using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Projectiles
{
	public class TorchClickerPro : ClickerProjectile
	{
		public int Timer
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public bool Spawned
		{
			get => Projectile.localAI[0] == 1f;
			set => Projectile.localAI[0] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.alpha = 255;
			Projectile.timeLeft = 300;
			Projectile.extraUpdates = 2;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override void AI()
		{
			int dustType = getTorchType(Main.player[Projectile.owner]);
			
			int index = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, dustType, 0f, 0f, 0, dustType == 66 ? Main.DiscoColor : default(Color), 1.5f);
			Dust dust = Main.dust[index];
			dust.position.X = Projectile.Center.X;
			dust.position.Y = Projectile.Center.Y;
			dust.velocity *= 0f;
			dust.noGravity = true;
			
			if (!Spawned)
			{
				Spawned = true;

				float max = 30f;
				int i = 0;
				while (i < max)
				{
					Vector2 vector2 = Vector2.Zero;
					vector2 += -Vector2.UnitY.RotatedBy(i * (MathHelper.TwoPi / max)) * new Vector2(2f, 2f);
					vector2 = vector2.RotatedBy(Projectile.velocity.ToRotation(), default(Vector2));
					int index2 = Dust.NewDust(Projectile.Center, 0, 0, dustType, 0f, 0f, 0, default(Color), 1.5f);
					Dust dust2 = Main.dust[index2];
					dust2.noGravity = true;
					dust2.position = Projectile.Center + vector2;
					dust2.velocity = vector2.SafeNormalize(Vector2.UnitY) * 2f;
					i++;
				}
			}
		}

		private int getTorchType(Player player)
		{
			int returnType = 6; // Torch

			if (player.ZoneHallow)
			{
				if (player.ZoneRockLayerHeight) returnType = 297; // Hallowed Torch
				else returnType = 66; // Rainbow Torch
			}
			else if (player.ZoneCorrupt)
			{
				if (player.ZoneRockLayerHeight) returnType = 75; // Cursed Torch
				else returnType = 295; // Corrupt Torch
			}
			else if (player.ZoneCrimson)
			{
				if (player.ZoneRockLayerHeight) returnType = 169; // Ichor Torch
				else returnType = 296; // Crimson Torch
			}
			else if (player.ZoneDungeon) returnType = 234; // Bone Torch
			else if (player.ZoneUnderworldHeight) returnType = 65; // Demon Torch
			else if (player.ZoneJungle || player.ZoneLihzhardTemple) returnType = 298; // Jungle Torch
			else if (player.ZoneUndergroundDesert || player.ZoneDesert) returnType = 293; // Desert Torch
			else if (player.ZoneSnow) returnType = 135; // Ice Torch
			else if (player.ZoneBeach) returnType = 294; // Coral Torch

			return returnType;
		}
	}
}
