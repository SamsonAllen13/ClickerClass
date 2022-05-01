using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Utilities
{
	internal static class ProjectileHelper
	{
		/// <summary>
		/// Moves the projectile in a sine wave pattern around it's original velocity, given a timer, the amplitude and the length of each step through the wave.
		/// </summary>
		/// <param name="projectile">The projectile to move.</param>
		/// <param name="timer">The timer used to calculate the wave movement.</param>
		/// <param name="amplitude">The wave's amplitude. Larger values will make the projectile move faster overall.</param>
		/// <param name="waveStep">The wave's length modifier. Values between PI and 2PI will have strange effects.</param>
		/// <param name="firstTick">If it is the first tick of the wave movement. The method then uses the projectile velocity as the center of the wave.</param>
		/// <param name="changeDirection">Allows modifying the original velocity of the projectile.</param>
		/// <param name="reverseWave">Whether to reverse the wave's motion this iteration.</param>
		public static void SineWaveMovement(this Projectile projectile, float timer, float amplitude, float waveStep, bool firstTick, Action<Projectile> changeDirection = null, bool reverseWave = false)
		{
			float time = timer * waveStep;
			float curHeight = (float)Math.Sin(time) * amplitude;

			float realSpeed;
			float realRot;

			if (firstTick)
			{
				realSpeed = projectile.velocity.Length();
				realRot = projectile.velocity.ToRotation();
			}
			else
			{
				float heightDiff = curHeight - (float)Math.Sin(time - waveStep) * amplitude;
				realSpeed = (float)Math.Sqrt(projectile.velocity.LengthSquared() - heightDiff * heightDiff);
				realRot = projectile.velocity.RotatedBy(-(new Vector2(realSpeed, heightDiff).ToRotation())).ToRotation();
			}

			if (changeDirection != null)
			{
				projectile.velocity = realRot.ToRotationVector2() * realSpeed;
				changeDirection(projectile);
				realRot = projectile.velocity.ToRotation();
				realSpeed = projectile.velocity.Length();
			}

			if (reverseWave)
			{
				amplitude *= -1;
				curHeight *= -1;
			}
			projectile.velocity = new Vector2(realSpeed, (float)Math.Sin(time + waveStep) * amplitude - curHeight).RotatedBy(realRot);
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
		}

		/// <summary>
		/// Calculates simple collision with NPCs, and manages two sets of collections pertaining to them so that damage related code runs properly.
		/// <para>Requires persistent collections for hitTargets and foundTargets, and the method to be called in any AI hook only once (per tick).</para>
		/// <para>This method only works on several assumptions:</para>
		/// <para>* local immunity is used, so that the first contact is a guaranteed hit</para>
		/// <para>* it uses generic "damage through contact" code, and not special laser code (last prism)</para>
		/// </summary>
		/// <param name="projectile">The projectile</param>
		/// <param name="hitTargets">The collection of NPC indexes it has already hit and should be excluded</param>
		/// <param name="foundTargets">The collection of NPC indexes from the last method call to then add to hitTargets</param>
		/// <param name="max">The amount of NPCs it can hit before killing itself</param>
		/// <param name="condition">Custom condition to check for the NPC before setting it as a suitable target. If null, counts as true.</param>
		/// <returns>True if target count is atleast max (which kills itself)</returns>
		public static bool HandleChaining(this Projectile projectile, ICollection<int> hitTargets, ICollection<int> foundTargets, int max, Func<NPC, bool> condition = null)
		{
			//Applies foundTargets from the last call to hitTargets
			foreach (var f in foundTargets)
			{
				if (!hitTargets.Contains(f))
				{
					//If check can be removed here, but left in in case of debugging/additional method
					hitTargets.Add(f);
				}
			}
			foundTargets.Clear();

			//Seek suitable targets the projectile collides with
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];

				if (!npc.active || npc.dontTakeDamage || projectile.friendly && npc.townNPC) //The only checks that truly prevent damage. No chaseable or immortal, those can still be hit
				{
					continue;
				}

				//Simple code instead of complicated recreation of vanilla+modded collision code here (which only runs clientside, but this has to be all-side)
				//Hitbox has to be for "next update cycle" because AI (where this should be called) runs before movement updates, which runs before damaging takes place
				Rectangle hitbox = new Rectangle((int)(projectile.position.X + projectile.velocity.X), (int)(projectile.position.Y + projectile.velocity.Y), projectile.width, projectile.height);
				ProjectileLoader.ModifyDamageHitbox(projectile, ref hitbox);

				if (!projectile.Colliding(hitbox, npc.Hitbox)) //Intersecting hitboxes + special checks. Safe to use all-side, lightning aura uses it
				{
					continue;
				}

				if (condition != null && !condition(npc))
				{
					//If custom condition returns false
					continue;
				}

				foundTargets.Add(i);
			}

			if (hitTargets.Count >= max)
			{
				projectile.Kill();
				return true;
			}

			return false;
		}

		/// <summary>
		/// Syncs the projectile through the net, as long as the current client is the owner. Use in conjunction with non-AI related hooks as netUpdate = true only works there.
		/// </summary>
		/// <param name="projectile"></param>
		public static void NetSync(this Projectile projectile)
		{
			projectile.netUpdate = projectile.netUpdate2 = false;
			if (projectile.netSpam < 60)
			{
				projectile.netSpam += 5;
			}
			if (Main.netMode != NetmodeID.SinglePlayer && projectile.owner == Main.myPlayer)
			{
				NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projectile.whoAmI);
			}
		}
	}
}
