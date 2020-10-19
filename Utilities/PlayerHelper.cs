using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Utilities
{
	public static class PlayerHelper
	{
		public static ClickerPlayer GetClickerPlayer(this Player player)
		{
			return player.GetModPlayer<ClickerPlayer>();
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
