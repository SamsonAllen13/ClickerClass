using ClickerClass.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using ReLogic.Content;

namespace ClickerClass.Projectiles
{
	public class BalloonClickerPro : ClickerProjectile
	{
		public static Asset<Texture2D> effect;

		public override void Load()
		{
			effect = Mod.Assets.Request<Texture2D>("Projectiles/BalloonClickerPro_Effect");
		}

		public override void Unload()
		{
			effect = null;
		}
		
		public bool hasChanged = false;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 26;
			Projectile.height = 26;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 255;
			Projectile.tileCollide = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture = effect.Value;
			Rectangle frame = texture.Frame(1, 1, frameY: 0);
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			if (!hasChanged)
			{
				Main.spriteBatch.Draw(texture, new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 20) - Main.screenPosition, null, lightColor, Projectile.rotation, frame.Size() / 2, Projectile.scale, SpriteEffects.None, 0f);
			}
			
			//TODO dire / Diver - Remove need for PreDraw nonsense since I don't know the new form of drawOriginOffsetY / drawOffsetX
			texture = TextureAssets.Projectile[Projectile.type].Value;
			frame = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);
			drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			SpriteEffects effects = Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Main.spriteBatch.Draw(texture, new Vector2(Projectile.Center.X, Projectile.Center.Y + 2) - Main.screenPosition, frame, lightColor, Projectile.rotation, frame.Size() / 2, Projectile.scale, effects, 0f);
			return true;
		}

		//TODO dire / Diver - Make the balloon phase influenced by wind and fix it's trouble getting through platforms

		public override void AI()
		{
			if (Projectile.ai[0] == 1f)
			{
				hasChanged = true;
			}
			
			if (hasChanged)
			{
				Projectile.aiStyle = 26;
				Projectile.velocity.Y += 0.1f;
				AIType = ProjectileID.BabySlime;
				if (Projectile.timeLeft < 30)
				{
					Projectile.alpha += 8;
				}
			}
			else
			{
				if (Projectile.timeLeft > 510)
				{
					Projectile.velocity.Y *= 0.96f;
				}
			}
			
			if (Projectile.timeLeft < 30)
			{
				Projectile.alpha += 8;
			}
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			fallThrough = false;
			return true;
		}

		public override void Kill(int timeLeft)
		{
			for (int u = 0; u < 10; u++)
			{
				int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y - 20), 20, 20, 33, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 100, default(Color), 1.5f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}
