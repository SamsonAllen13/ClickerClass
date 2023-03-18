using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class MiceDrillPro : ModProjectile
	{
		public static Lazy<Asset<Texture2D>> glowmask;

		public override void Load()
		{
			glowmask = new(() => ModContent.Request<Texture2D>(Texture + "_Glow"));
		}

		public override void Unload()
		{
			glowmask = null;
		}

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 26;
			Projectile.height = 54;
			Projectile.aiStyle = 20;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.hide = true; //aiStyle 20 assigns heldProj
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
		}

		public override void PostDraw(Color lightColor)
		{
			//TODO Generic glowmask draw, maybe generalize method
			Player player = Main.player[Projectile.owner];

			int offsetY = 0;
			int offsetX = 0;
			Texture2D glowmaskTexture = glowmask.Value.Value;
			float originX = (glowmaskTexture.Width - Projectile.width) * 0.5f + Projectile.width * 0.5f;
			ProjectileLoader.DrawOffset(Projectile, ref offsetX, ref offsetY, ref originX);

			SpriteEffects spriteEffects = SpriteEffects.None;
			if (Projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}

			if (Projectile.ownerHitCheck && player.gravDir == -1f)
			{
				if (player.direction == 1)
				{
					spriteEffects = SpriteEffects.FlipHorizontally;
				}
				else if (player.direction == -1)
				{
					spriteEffects = SpriteEffects.None;
				}
			}

			Vector2 drawPos = new Vector2(Projectile.position.X - Main.screenPosition.X + originX + offsetX, Projectile.position.Y - Main.screenPosition.Y + Projectile.height / 2 + Projectile.gfxOffY);
			Rectangle sourceRect = glowmaskTexture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
			Color glowColor = new Color(255, 255, 255, 255) * 0.7f * Projectile.Opacity;
			Vector2 drawOrigin = new Vector2(originX, Projectile.height / 2 + offsetY);
			Main.EntitySpriteDraw(glowmaskTexture, drawPos, sourceRect, glowColor, Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
		}
	}
}
