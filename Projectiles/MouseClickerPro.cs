using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using ClickerClass.Utilities;

namespace ClickerClass.Projectiles
{
	public class MouseClickerPro : ClickerProjectile
	{
		private int oldStuckState = 0;

		public int StuckState
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public bool HasSpawnEffects
		{
			get => Projectile.ai[1] == 1f;
			set => Projectile.ai[1] = value ? 1f : 0f;
		}

		public int TargetIndex
		{
			get => (int)Projectile.ai[2];
			set => Projectile.ai[2] = value;
		}

		public const int aliveTime = 20 * 60;
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
			Projectile.timeLeft = aliveTime;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
			
			DrawOriginOffsetY = 4;
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			if (!target.active || target.ImmuneToAllBuffs())
			{
				return;
			}

			int otherCount = 0;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile proj = Main.projectile[i];

				if (proj.active && proj.type == Projectile.type && proj.ModProjectile is MouseClickerPro mouseClicker &&
					mouseClicker.StuckState == 1 && mouseClicker.TargetIndex == target.whoAmI)
				{
					otherCount++;
				}
			}

			//Prevent sticking more than 5
			if (otherCount >= 5)
			{
				return;
			}

			StuckState = 1;
			TargetIndex = target.whoAmI;
			Projectile.velocity = (target.Center - Projectile.Center) * 0.75f;
			Projectile.netUpdate = true;
			Projectile.friendly = false;
		}

		public override void AI()
		{
			bool killProj = false;
			Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;

			if (HasSpawnEffects)
			{
				SoundEngine.PlaySound(SoundID.Item152, Projectile.Center);
				HasSpawnEffects = false;
			}

			if (StuckState == 0) //Projectile -IS NOT- sticking to an enemy
			{
				Projectile.velocity.X *= 0.975f;
				Projectile.velocity.Y += 0.155f;
			}
			else if (StuckState == 1) //Projectile -IS- sticking to an enemy
			{
				if (oldStuckState != 1)
				{
					SoundEngine.PlaySound(SoundID.Item153, Projectile.Center);
				}

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
					globalNPC.mouseTrapped++;
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

			oldStuckState = StuckState;
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
			for (int k = 0; k < 5; k++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, DustID.Stone, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), 75, default, 1f);
				dust.noGravity = true;
			}
		}
	}
}
