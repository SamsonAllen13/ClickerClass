using ClickerClass.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using ClickerClass.Utilities;

namespace ClickerClass.Projectiles
{
	public class PharaohsClickerPro : ClickerProjectile
	{
		protected int bobTimer = 0;
		protected const int bobTimerMax = 150;
		protected const float bobRotOffset = 0.3f;
		
		public float BobFactorPos => (float)bobTimer / bobTimerMax;

		public float BobFactorRot => (BobFactorPos + bobRotOffset) % 1;

		public float BobPosSin => (float)Math.Sin(BobFactorPos * MathHelper.TwoPi);

		public Vector2 BobPosOffset => new Vector2(0, BobPosSin * 6);

		public float BobRotationSin => (float)Math.Sin(BobFactorRot * MathHelper.TwoPi);
		
		public bool HasSpawnEffects
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}
		
		public int clickCountCheck = 0;
		
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 34;
			Projectile.height = 34;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
			Projectile.alpha = 0;
		}
		
		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);

			Vector2 offset = BobPosOffset;
			Vector2 drawPos = Projectile.Center - Main.screenPosition + offset;
			float rotation = Projectile.rotation;
			float scale = Projectile.scale;
			SpriteEffects effects = Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			//Manually draw projectile
			Main.EntitySpriteDraw(texture, drawPos, null, lightColor * Projectile.Opacity, (float)rotation, drawOrigin, (float)scale, effects, 0);
			return false;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			
			if (HasSpawnEffects)
			{
				//Avoids weird first shot
				clickCountCheck = clickerPlayer.clickerTotal;
				
				SoundEngine.PlaySound(SoundID.Item43, Projectile.Center);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(4), 8, 8, 32, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 125, default, 2f);
					dust.noGravity = true;
				}
				HasSpawnEffects = false;
			}
			
			bobTimer++;
			if (bobTimer >= bobTimerMax)
			{
				bobTimer = 0;
			}
			
			Projectile.rotation = Projectile.velocity.X * 0.05f;
			Projectile.rotation += BobRotationSin * 0.08f;
			Projectile.alpha += Projectile.timeLeft < 30 ? 8 : 0;
			
			MousePlayer mousePlayer = Main.player[Projectile.owner].GetModPlayer<MousePlayer>();
			if (mousePlayer.TryGetMousePosition(out Vector2 mouseWorld))
			{
				Projectile.spriteDirection = mouseWorld.X > Projectile.position.X ? 1 : -1;
				
				if (clickerPlayer.clickerTotal > clickCountCheck && Main.myPlayer == Projectile.owner)
				{
					Vector2 vector = mouseWorld - (Projectile.Center + BobPosOffset);
					float speed = 3f;
					float mag = vector.Length();
					if (mag > speed)
					{
						mag = speed / mag;
						vector *= mag;
					}
					//Deals 1 damage
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + BobPosOffset, vector, ModContent.ProjectileType<PharaohsClickerPro2>(), 1, 1f, Projectile.owner, 1f);
				}
			}
			clickCountCheck = clickerPlayer.clickerTotal;
		}
	}
}
