using ClickerClass.Core.EntitySources;
using ClickerClass.Core.Netcode.Packets;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
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
		public static int HealLife(this Player player, int healAmount, Player healer = null, bool healOverMax = false)
		{
			int real = 0;
			if (healer == null)
			{
				healer = player;
			}
			if (healer.whoAmI != Main.myPlayer && Main.netMode != NetmodeID.Server)
			{
				//If called as a non-healer client
				return real;
			}
			if (!healOverMax && player.statLife >= player.statLifeMax2 || healAmount <= 0)
			{
				return real;
			}
			real = healAmount;
			//Clamp to prevent buggy mods from causing "negative heal"
			int healClamped = Math.Max(0, Math.Min(real, player.statLifeMax2 - player.statLife));
			if (!healOverMax && player.statLifeMax2 < healAmount + player.statLife)
			{
				real = healClamped;
			}

			player.statLife += real;

			if (healOverMax && player.statLife > player.statLifeMax2)
			{
				player.statLife = player.statLifeMax2;
			}
			if (healer.whoAmI == Main.myPlayer)
			{
				player.HealEffect(real, true);
			}
			if (player.whoAmI != Main.myPlayer)
			{
				//MessageID.SpiritHeal: Increases statLife by the passed in amount, then calls HealEffect, and if server, sends it to the targeted player
				NetMessage.SendData(MessageID.SpiritHeal, -1, -1, null, player.whoAmI, real);
			}
			return real;
		}

		public const int CooldownSlot_All = sbyte.MaxValue;

		/// <summary>
		/// Call to set a player's immunity timers. Defaults to 60 + 30 immune time and blinking.
		/// <para>If this method is called elsewhere and request is true, netcode will be initiated.</para>
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="time">The immune time.</param>
		/// <param name="longTime">The additional immune time when longInvince is true (cross necklace).</param>
		/// <param name="noBlink">Set to true to make it so the player doesn't blink during immunity.</param>
		/// <param name="cooldownID">The <see cref="ImmunityCooldownID"/>. Default for applying all.</param>
		/// <param name="combatText">Optional CombatText. defaults to "", which means no text</param>
		/// <param name="combatTextColor">Parameter for CombatText.</param>
		/// <param name="dramatic">Parameter for CombatText.</param>
		/// <param name="dot">Parameter for CombatText.</param>
		/// <param name="request">Set to true if you are calling this on another player in multiplayer, or the server. Concider checking myPlayer or netmode.</param>
		public static void SetImmune(this Player player, short time = 60, short longTime = 30, bool noBlink = false, int cooldownID = CooldownSlot_All, string combatText = "", Color combatTextColor = default(Color), bool dramatic = false, bool dot = false, bool request = false)
		{
			if (player.whoAmI != Main.myPlayer && request && Main.netMode != NetmodeID.SinglePlayer)
			{
				//If this is called on another player (in multiplayer) and a request is made
				int toWho = -1;
				//Client will send it to server, server will send the packet to the player required
				new SetImmunePacket(player, time, longTime, noBlink, cooldownID, combatText, combatTextColor, dramatic, dot).Send(toWho);

				//No action should be taken, in all cases clients end up executing this without request = true, so the below code will run
				return;
			}

			if (Main.netMode == NetmodeID.Server)
			{
				//Servers don't need to do any immunity code
				return;
			}

			player.immune = true;
			int combinedTime = time;
			if (player.longInvince)
			{
				combinedTime += longTime;
			}
			if (cooldownID == -1 || cooldownID == CooldownSlot_All)
			{
				player.immuneTime = combinedTime;

				if (cooldownID == CooldownSlot_All)
				{
					for (int i = 0; i < player.hurtCooldowns.Length; i++)
					{
						player.hurtCooldowns[i] = combinedTime;
					}
				}
			}
			else if (cooldownID >= 0)
			{
				player.hurtCooldowns[cooldownID] = combinedTime;
			}
			player.immuneNoBlink = noBlink;

			if (!string.IsNullOrEmpty(combatText))
			{
				CombatText.NewText(player.getRect(), combatTextColor, combatText, dramatic, dot);
			}
		}

		//Helper method
		public static IEntitySource GetSource_Accessory_OnHit(this Player player, Item item, Entity victim, string context = null)
		{
			return new EntitySource_ItemUse_OnHit(player, item, victim, context);
		}
	}
}
