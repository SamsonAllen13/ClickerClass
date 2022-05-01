using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace ClickerClass.Utilities
{
	public static class PlayerHelper
	{
		public static ClickerPlayer GetClickerPlayer(this Player player)
		{
			return player.GetModPlayer<ClickerPlayer>();
		}

		/// <summary>
		/// Carbon copy of the vanilla Player.Teleport, without effects. Only handles basic code and effects, no netcode
		/// </summary>
		/// <param name="player">The player</param>
		/// <param name="newPos">The teleport position</param>
		public static void ClickerTeleport(this Player player, Vector2 newPos)
		{
			player.RemoveAllGrapplingHooks();

			float distance = Vector2.Distance(player.Center, newPos);
			PressurePlateHelper.UpdatePlayerPosition(player);
			player.Center = newPos;
			player.fallStart = (int)(player.position.Y / 16f);
			if (player.whoAmI == Main.myPlayer)
			{
				bool offScreen = true;
				if (distance < new Vector2(Main.screenWidth, Main.screenHeight).Length() / 2f + 100f)
				{
					Main.SetCameraLerp(0.1f, 0);
					offScreen = false;
				}
				else
				{
					Main.BlackFadeIn = 255;
					Lighting.Clear();
					Main.screenLastPosition = Main.screenPosition;
					Main.screenPosition = player.Center - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
					Main.instantBGTransitionCounter = 10;
				}
				if (offScreen)
				{
					if (Main.mapTime < 5)
					{
						Main.mapTime = 5;
					}

					Main.maxQ = true;
					Main.renderNow = true;
				}
			}

			PressurePlateHelper.UpdatePlayerPosition(player);
			for (int j = 0; j < 3; j++)
			{
				player.UpdateSocialShadow();
			}

			player.oldPosition = player.position + player.BlehOldPositionFixer;
		}

		/// <summary>
		/// Heals the player's life by the given amount. If the amount is less or equal to 0, it does nothing.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="healAmount">The amount to heal the player by.</param>
		/// <param name="healer">The other player healing this player. If null, defaults to self healing.</param>
		/// <param name="healOverMax">Whether the healing is always shown regardless of healing amount going over maximum health. defaults to false.</param>
		public static void HealLife(this Player player, int healAmount, Player healer = null, bool healOverMax = false)
		{
			if (!healOverMax && player.statLife >= player.statLifeMax2 || healAmount <= 0)
			{
				return;
			}
			if (!healOverMax && player.statLifeMax2 < healAmount + player.statLife)
			{
				healAmount = player.statLifeMax2 - player.statLife;
			}
			player.statLife += healAmount;
			if (healOverMax && player.statLife > player.statLifeMax2)
			{
				player.statLife = player.statLifeMax2;
			}
			if (healer == null)
			{
				healer = player;
			}
			if (healer.whoAmI == Main.myPlayer)
			{
				player.HealEffect(healAmount);
			}
			if (player.whoAmI != Main.myPlayer)
			{
				NetMessage.SendData(MessageID.SpiritHeal, -1, -1, null, player.whoAmI, healAmount);
			}
		}
	}
}
