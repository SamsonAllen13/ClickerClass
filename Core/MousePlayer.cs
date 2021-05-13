using ClickerClass.Core.Netcode.Packets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Core
{
	/// <summary>
	/// Helper ModPlayer used to wrap around Main.MouseWorld so it's multiplayer compatible
	/// </summary>
	public class MousePlayer : ModPlayer
	{
		/*
		 * How this is used:
		 * - Whenever something wants client A's mouse position, access A's players ModPlayer, and calls {GetMousePosition()}
		 *     - It can be null! (if client B hasn't received A's mouse position yet)
		 * 
		 * How this works:
		 * - Client A makes it clear that he wants his mouse position to sync, initiates sending process (by using {SetMousePosition()})
		 *     - Sending process is send MouseWorld every {updateRate} ticks while client wants it
		 * - Server receives, resends to clients (one of them B)
		 *     - B receives {NextMousePosition}, and:
		 *         - if it's the first position received: calls {SetNextMousePosition()}
		 *         - else: use UpdateRule() to move {MousePosition} towards the received value
		 *     - If B holds a MousePosition and timeout is reached (no more incoming packets hopefully):
		 *         - nulls {MousePosition} and sets related fields to default
		 */

		public static readonly Vector2 MaxRange = new Vector2(1920, 1080) * 3;

		private bool sentThisTick;

		/// <summary>
		/// Primarily used clientside to send regular updates to the server
		/// </summary>
		private int updateRate;

		/// <summary>
		/// Timeout threshold for when to stop expecting updates for mouse position
		/// </summary>
		private int timeout;

		private int timeoutTimer;

		/// <summary>
		/// "Real" mouse position
		/// </summary>
		private Vector2? MousePosition = null;

		/// <summary>
		/// Targeted mouse position
		/// </summary>
		private Vector2? NextMousePosition = null;

		/// <summary>
		/// Previous position (to reset timeout)
		/// </summary>
		private Vector2? OldNextMousePosition = null;

		public override void Initialize()
		{
			Reset();
			timeout = 30;
			updateRate = 5;
			sentThisTick = false;
		}

		public override void PostUpdate()
		{
			UpdateMousePosition();
		}

		/// <summary>
		/// Assigns mousePosition to this player's mouse position, accurate if singleplayer/client, interpolated if other client, or Vector2.Zero if not available (will return false in that case)
		/// </summary>
		/// <param name="mousePosition"></param>
		/// <returns>True if mouse position exists</returns>
		public bool TryGetMousePosition(out Vector2 mousePosition)
		{
			mousePosition = Vector2.Zero;
			if (player.whoAmI == Main.myPlayer)
			{
				mousePosition = Main.MouseWorld;
				return true;
			}
			if (MousePosition != null)
			{
				mousePosition = MousePosition.Value;
				return true;
			}
			return false;
		}

		/// <summary>
		/// If local client, initiates netcode. Always call this before using <see cref="TryGetMousePosition"/>, since that method doesn't "request" anything.
		/// </summary>
		public void SetMousePosition()
		{
			if (Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI == Main.myPlayer)
			{
				if (!sentThisTick && (NextMousePosition == null || Main.GameUpdateCount % updateRate == 0))
				{
					//If hasn't sent a mouse position recently, or when an update is required
					sentThisTick = true;

					//Send packet
					if (NextMousePosition != Main.MouseWorld)
					{
						new MousePacket(player, Main.MouseWorld).Send();
					}
					else
					{
						//If mouse position didn't change, reset timeout and send a packet that does the same
						ResetTimeout();
						new MouseResetTimeoutPacket(player).Send();
					}

					//Required so client also updates this variable even though its not used directly
					NextMousePosition = Main.MouseWorld;
				}
			}
		}

		/// <summary>
		/// Called on receiving latest mouse position by server or other clients
		/// </summary>
		public void SetNextMousePosition(Vector2 position)
		{
			if (player.whoAmI != Main.myPlayer)
			{
				NextMousePosition = position;
			}
		}

		/// <summary>
		/// Called on receiving latest keepalive packet by server or other clients
		/// </summary>
		public void ResetTimeout()
		{
			timeoutTimer = 0;
		}

		/// <summary>
		/// Return a new position based on current and final
		/// </summary>
		private Vector2 UpdateRule(Vector2 current, Vector2 final)
		{
			return Vector2.Lerp(current, final, 2 * 1f / updateRate);
		}

		/// <summary>
		/// Clears all sync related fields
		/// </summary>
		private void Reset()
		{
			MousePosition = null;
			NextMousePosition = null;
			OldNextMousePosition = null;
			timeoutTimer = 0;
		}

		private void UpdateMousePosition()
		{
			sentThisTick = false;

			if (NextMousePosition == null)
			{
				//No pending position to sync
				return;
			}
			else if (MousePosition == null)
			{
				//New incoming position
				MousePosition = NextMousePosition;
				timeoutTimer++;
				return;
			}

			//If here:
			// -Mouse needs updating
			// -All related things aren't null

			if (timeoutTimer++ < timeout)
			{
				if (OldNextMousePosition != NextMousePosition)
				{
					//Received new position: reset timeout timer
					timeoutTimer = 0;
				}

				OldNextMousePosition = NextMousePosition;

				if (MousePosition == null || NextMousePosition == null)
				{
					//Hard failsafe, shouldn't happen
					return;
				}

				//Vector2? to Vector2 conversion
				Vector2 mousePos = MousePosition ?? Vector2.Zero;
				Vector2 nextMousePos = NextMousePosition ?? Vector2.Zero;

				MousePosition = UpdateRule(mousePos, nextMousePos);
			}
			else
			{
				Reset();
			}
		}
	}
}
