using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using ClickerClass.Utilities;

namespace ClickerClass.Projectiles
{
	public class MouseClickerPro : ClickerProjectile
	{
		//TODO 1.4.4 - Sprite isnt finished
		
		public float hitState = 0f;

		public float Timer
		{
			get => hitState;
			set => hitState = value;
		}

		public int StuckState
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public int TargetIndex
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		//This referencing ai[1] again works because it is repurposed when StuckState changes
		public float AmpCount
		{
			get => Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public const int stuckTime = 10 * 60;

		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.penetrate = 3;
			Projectile.friendly = true;
			Projectile.timeLeft = 1200;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
			
			DrawOriginOffsetY = 4;
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			var globalNPC = target.GetClickerGlobalNPC();
			if (target.active && !target.ImmuneToAllBuffs() && globalNPC.mouseTrapped < 5)
			{
				SoundEngine.PlaySound(SoundID.Item153, target.Center);
				
				StuckState = 1;
				TargetIndex = target.whoAmI;
				Projectile.velocity = (target.Center - Projectile.Center) * 0.75f;
				Projectile.netUpdate = true;
				Projectile.friendly = false;
			}
		}

		public override void AI()
		{
			//TODO - Move sound to on-projectile-spawn
			
			bool killProj = false;
			Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;

			if (StuckState == 0) //Projectile -IS NOT- sticking to an enemy
			{
				Projectile.velocity.X *= 0.975f;
				Projectile.velocity.Y += 0.155f;
			}
			else if (StuckState == 1) //Projectile -IS- sticking to an enemy
			{
				Projectile.extraUpdates = 0;
				Projectile.frame = 1;
				Projectile.ignoreWater = true;
				Projectile.tileCollide = false;
				Projectile.localAI[0] += 1f;
				int projTargetIndex = TargetIndex;
				if (Projectile.localAI[0] >= stuckTime)
				{
					killProj = true;
				}

				if (projTargetIndex < 0 || projTargetIndex >= 200)
				{
					killProj = true;
				}
				else if (Main.npc[projTargetIndex] is NPC npc && npc.active && !npc.dontTakeDamage)
				{
					Projectile.Center = npc.Center - Projectile.velocity * 2f;
					Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
					Projectile.gfxOffY = npc.gfxOffY;

					var globalNPC = npc.GetClickerGlobalNPC();
					if (globalNPC.mouseTrapped < 5)
					{
						globalNPC.mouseTrapped++;
					}
				}
				else
				{
					killProj = true;
				}
			}
			else
			{
				Projectile.localAI[0] += 1f;
				if (Projectile.localAI[0] >= 60)
				{
					killProj = true;
				}
			}

			if (killProj)
			{
				Projectile.Kill();
			}
		}
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			fallThrough = false;

			return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}

		public override void OnKill(int timeLeft)
		{
			//TODO visuals when disappearing
		}
	}
}
