using Microsoft.Xna.Framework;
using System;
using Terraria;

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
	}
}
