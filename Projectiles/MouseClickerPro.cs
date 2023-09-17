using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using ClickerClass.Utilities;

namespace ClickerClass.Projectiles
{
	public class MouseClickerPro : ClickerProjectile
	{
		//TODO 1.4.4 - Code not finished, sprite isnt finished, etc.
		
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

		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.penetrate = 3;
			Projectile.friendly = true;
			Projectile.timeLeft = 900;
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			StuckState = 1;
			TargetIndex = target.whoAmI;
			Projectile.velocity = (target.Center - Projectile.Center) * 0.75f;
			Projectile.netUpdate = true;
			Projectile.friendly = false;
			
			var globalNPC = target.GetClickerGlobalNPC();
			int stack = globalNPC.mouseTrapped;
			if (target.active && !target.ImmuneToAllBuffs() && stack < 5)
			{
				stack++;
				//globalNPC.ApplyMouseTrappedStack(target, Main.player[Projectile.owner], stack, true);
			}
		}

		public override void AI()
		{
			bool killProj = false;

			if (StuckState == 0) //Projectile -IS NOT- sticking to an enemy
			{
				Projectile.rotation += Projectile.velocity.X * 0.05f;
				
				Projectile.velocity.X *= 0.99f;
				Projectile.velocity.Y += 0.155f;
			}
			else if (StuckState == 1) //Projectile -IS- sticking to an enemy
			{
				Projectile.extraUpdates = 0;
				Projectile.frame = 1;
				Projectile.rotation = 0f;
				Projectile.ignoreWater = true;
				Projectile.tileCollide = false;
				Projectile.localAI[0] += 1f;
				int projTargetIndex = TargetIndex;
				if (Projectile.localAI[0] >= 300)
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
					Projectile.gfxOffY = npc.gfxOffY;
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
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}

		public override void OnKill(int timeLeft)
		{
			
		}
	}
}
