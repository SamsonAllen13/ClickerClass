using ClickerClass.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ClickerClass.Projectiles
{
	public class FaultyClickerPro : ClickerProjectile
	{
		public bool Spawned
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}
		
		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.aiStyle = -1;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 6;
		}

		public override void AI()
		{
			if (!Spawned)
			{
				Spawned = true;
				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 94);
			}
		}
		
		public override void Kill(int timeLeft)
		{
			float num102 = 25f;
			int num103 = 0;
			while ((float)num103 < num102)
			{
				Vector2 vector12 = Vector2.UnitX * 0f;
				vector12 += -Vector2.UnitY.RotatedBy((double)((float)num103 * (6.28318548f / num102)), default(Vector2)) * new Vector2(4f, 4f);
				vector12 = vector12.RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
				int num104 = Dust.NewDust(Projectile.Center, 0, 0, 229, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num104].noGravity = true;
				Main.dust[num104].position = Projectile.Center + vector12;
				Main.dust[num104].velocity = Projectile.velocity * 0f + vector12.SafeNormalize(Vector2.UnitY) * 2f;
				int num = num103;
				num103 = num + 1;
			}
		}
	}
}
