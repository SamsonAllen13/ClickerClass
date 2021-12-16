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
	public class StarryClickerPro : ClickerProjectile
	{
		public static Asset<Texture2D> effect;

		public override void Load()
		{
			effect = Mod.Assets.Request<Texture2D>("Projectiles/StarryClickerPro_Effect");
		}

		public override void Unload()
		{
			effect = null;
		}
		
		public int DestinationX
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
		
		public int DestinationY
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
			Projectile.tileCollide = false;
		}
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			width = Projectile.width / 2;
			height = Projectile.height / 2;
			return true;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 100) * Projectile.Opacity;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture = effect.Value;
			Rectangle frame = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + new Vector2(frame.Width / 2, frame.Height * 0.2f) + new Vector2(0f, Projectile.gfxOffY);
				Main.EntitySpriteDraw(texture, drawPos, null, new Color(255, 255, 255, 100) * ((8 - k) * 0.025f), 0f, new Vector2(frame.Width / 2, frame.Height * 0.8f), Projectile.scale, SpriteEffects.None, 0);
			}
			
			texture = TextureAssets.Projectile[Projectile.type].Value;
			frame = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, new Color(255, 255, 255, 0) * (Projectile.Opacity * 0.5f), -Projectile.rotation, frame.Size() / 2, Projectile.scale, SpriteEffects.None, 0);
			return true;
		}
		
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			crit = true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			if (Projectile.localAI[0] == 0f)
			{
				Projectile.localAI[0] = 1f;

				SoundEngine.PlaySound(SoundID.Item, (int)player.Center.X, (int)player.Center.Y, 9);
				for (int k = 0; k < 20; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 71, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 0, default, 1.75f);
					dust.noGravity = true;
				}
			}
			
			Projectile.rotation += 0.1f;
			
			Vector2 vec = new Vector2(DestinationX, DestinationY);
			if (Projectile.DistanceSQ(vec) <= 10 * 10)
			{
				Projectile.tileCollide = true;
			}
		}

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item, (int)Projectile.Center.X, (int)Projectile.Center.Y, 8, volumeScale: 0.5f);
			for (int u = 0; u < 10; u++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 57, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 255, default(Color), 1.5f);
				Main.dust[dust].noGravity = true;
			}
			for (int u = 0; u < 15; u++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 71, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 255, default(Color), 1.5f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}
