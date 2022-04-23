using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.GameContent;
using ReLogic.Content;

namespace ClickerClass.Projectiles
{
	public class CyclopsClickerPro : ClickerProjectile
	{
		public float rotationSpeed = 0.25f;
		public static Asset<Texture2D> effect;

		public override void Load()
		{
			effect = Mod.Assets.Request<Texture2D>("Projectiles/CyclopsClickerPro_Effect");
		}

		public override void Unload()
		{
			effect = null;
		}
		
		public bool HasSpawnEffects
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 88;
			Projectile.height = 88;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 100;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}
		
		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture = effect.Value;
			SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			Rectangle frame = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);
			Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, frame, new Color(255, 255, 255, 50) * (0.35f * Projectile.Opacity), Projectile.rotation, new Vector2(44, 44), 1.1f, spriteEffects, 0);
			return true;
		}
		
		//TODO - Figure out how to replicate Deerclops 'Insanity Hands' draw effect

		public override void AI()
		{
			if (HasSpawnEffects)
			{
				HasSpawnEffects = false;
				SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 43);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 54, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 125, default, 2f);
					dust.noGravity = true;
				}
			}
			Projectile.spriteDirection = Projectile.ai[1] > 0 ? 1 : -1;

			if (Projectile.timeLeft > 86)
			{
				Projectile.rotation -= Projectile.ai[1] * 0.05f;
			}
			else
			{
				Projectile.rotation += Projectile.ai[1] * rotationSpeed;
				rotationSpeed -= 0.0025f;
			}
			if (Projectile.timeLeft < 20)
			{
				Projectile.alpha += 10;
			}
		}
	}
}