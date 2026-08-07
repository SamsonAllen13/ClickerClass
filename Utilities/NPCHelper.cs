using Terraria;
using Terraria.ID;
using ClickerClass.NPCs;
using System;
using Terraria.GameContent.ItemDropRules;

namespace ClickerClass.Utilities
{
	internal static partial class NPCHelper
	{
		/// <summary>
		/// 
		/// </summary>
		public static ClickerGlobalNPC GetClickerGlobalNPC(this NPC npc)
		{
			return npc.GetGlobalNPC<ClickerGlobalNPC>();
		}

		/// <summary>
		/// Returns true if the NPC is flagged to be immune to all buffs (except tag buffs)
		/// </summary>
		/// <param name="npc"></param>
		/// <returns></returns>
		public static bool ImmuneToAllBuffs(this NPC npc)
		{
			return NPCID.Sets.ImmuneToRegularBuffs[npc.type];
		}
		
		/// <summary>
		/// Check if this NPC is tied to the healthpool of another NPC. Sets the parent if it exists.
		/// </summary>
		public static bool IsChild(this NPC npc, out NPC parent)
		{
			bool child = npc.realLife != npc.whoAmI && npc.realLife >= 0 && npc.realLife <= Main.maxNPCs;
			parent = child ? Main.npc[npc.realLife] : null;
			return child;
		}

		/// Copy of NPC.CanBeChasedBy without the active check. Use in contexts where the NPC might not be active anymore (such as OnHitNPC).
		/// </summary>
		/// <param name="npc">The NPC.</param>
		/// <param name="attacker">The attacker (unused).</param>
		/// <param name="ignoreDontTakeDamage">If dontTakeDamage should be ignored.</param>
		/// <returns>True if chaseable, max life > 5, not friendly, and can take damage.</returns>
		public static bool IsHostile(this NPC npc, object attacker = null, bool ignoreDontTakeDamage = false)
		{
			if (/*npc.active && */!npc.friendly && npc.lifeMax > 5 && npc.chaseable && (!npc.dontTakeDamage || ignoreDontTakeDamage))
				return !npc.immortal;

			return false;
		}

		/// <summary>
		/// Alternative version of <see cref="CommonCode.DropItemLocalPerClientAndSetNPCMoneyTo0"/>. Checks the condition delegate PER-PLAYER before syncing/spawning the item.
		/// </summary>
		public static void DropItemInstanced(DropAttemptInfo info, int itemType, int itemStack = 1, Func<DropAttemptInfo, bool> condition = null, bool interactionRequired = true)
		{
			if (itemType <= 0)
			{
				return;
			}

			NPC npc = info.npc;
			if (Main.netMode == NetmodeID.Server)
			{
				var origInfo = info;
				int item = Item.NewItem(npc.GetSource_Loot(), npc.getRect(), itemType, itemStack, true);
				Main.timeItemSlotCannotBeReusedFor[item] = 54000;
				for (int p = 0; p < Main.maxPlayers; p++)
				{
					if (Main.player[p].active && (npc.playerInteraction[p] || !interactionRequired))
					{
						//Manually switch the player instance
						info = origInfo;
						info.player = Main.player[p];

						if (condition?.Invoke(info) ?? true)
						{
							NetMessage.SendData(MessageID.InstancedItem, p, -1, null, item);
						}
					}
				}
				Main.item[item].active = false;
			}
			else if (Main.netMode == NetmodeID.SinglePlayer)
			{
				if (condition?.Invoke(info) ?? true)
				{
					CommonCode.DropItem(info, itemType, itemStack);
				}
			}
		}
	}
}
