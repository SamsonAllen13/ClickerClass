using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ReLogic.Content;
using System.IO;
using Terraria.Audio;

namespace ClickerClass.Projectiles
{
	public class NaughtyClickerPro2 : ClickerProjectile
	{
		public int GravityTimer
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 74);
			if (Main.myPlayer == Projectile.owner)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<NaughtyClickerPro3>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
			}
		}

		public override void AI()
		{
			Projectile.rotation += Projectile.velocity.X * 0.05f;

			Projectile.velocity.X *= 0.99f;

			GravityTimer++;
			if (GravityTimer >= 0)
			{
				Projectile.velocity.Y += 1.055f;
				GravityTimer = -10;
			}
			if (Projectile.timeLeft < 30)
			{
				Projectile.alpha += 8;
			}
			else if (Projectile.alpha < 150)
			{
				Projectile.alpha += 2;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}
	}
}
