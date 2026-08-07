using ClickerClass.Core.Netcode.Packets;
using ClickerClass.DropRules;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Core.Handlers.ClickerDamageDropHandler
{
	public class ClickerNPCDropGlobalNPC : GlobalNPC
	{
		//Populated during SetStaticDefaults
		private static Dictionary<int, ClickerNPCDropData> typeToData;

		//Exists for edge cases where multiple NPCs track loot from only 1 NPC, such as WOF. Only really takes effect in edge cases where you didn't damage the main NPC
		private static Dictionary<int, int> secondaryToMainNPCMapping;

		public override void Load()
		{
			if (typeToData == null)
			{
				typeToData = new();
				secondaryToMainNPCMapping = new();
			}
		}

		public override void Unload()
		{
			typeToData = null;
		}

		public static void AddDrop(int type, ClickerNPCDropData data)
		{
			if (type == NPCID.WallofFlesh)
			{
				secondaryToMainNPCMapping[NPCID.WallofFleshEye] = NPCID.WallofFlesh;
				//Other cases here when needed
			}

			if (typeToData.TryGetValue(type, out var existingData))
			{
				existingData.MergeWith(data);
			}
			else
			{
				typeToData[type] = data;
			}
		}

		//No syncing needed
		private HashSet<int> playersDamagedByClicker = new HashSet<int>();

		public override bool InstancePerEntity => true;

		public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
		{
			return typeToData.ContainsKey(entity.type) || secondaryToMainNPCMapping.ContainsKey(entity.type);
		}

		public override GlobalNPC Clone(NPC from, NPC to)
		{
			var clone = (ClickerNPCDropGlobalNPC)base.Clone(from, to);
			clone.playersDamagedByClicker = new HashSet<int>(from.GetGlobalNPC<ClickerNPCDropGlobalNPC>().playersDamagedByClicker);
			return clone;
		}

		public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
		{
			int playerWhoAmI = projectile.owner;
			if (ClickerSystem.IsClickerProj(projectile) && !playersDamagedByClicker.Contains(playerWhoAmI))
			{
				SetDamagedByClicker(npc, playerWhoAmI);
			}
		}

		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (!typeToData.TryGetValue(npc.type, out var data))
			{
				return;
			}

			foreach (var tuple in data.ItemTypeCondition)
			{
				npcLoot.Add(new DropLocalPerClientConditionPerPlayer(tuple.Item1, 1, 1, 1, tuple.Item2));
			}
		}

		public bool DamagedByPlayer(int playerWhoAmI) => playersDamagedByClicker.Contains(playerWhoAmI);

		public static void SetDamagedByClicker(NPC npc, int playerWhoAmI)
		{
			if (!npc.TryGetGlobalNPC<ClickerNPCDropGlobalNPC>(out var globalNPC))
			{
				return;
			}

			if (secondaryToMainNPCMapping.TryGetValue(npc.type, out var mainType))
			{
				//Naively apply this to any NPC of that type since there is no direct connection/reference
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC mainNPC = Main.npc[i];
					if (mainNPC.active && mainNPC.type == mainType)
					{
						SetDamagedByClicker(mainNPC, playerWhoAmI);
					}
				}
			}

			//Only the client applying this needs to know (to avoid spamming packet), so no rebroadcast by the server
			globalNPC.playersDamagedByClicker.Add(playerWhoAmI);

			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				new NPCDamagedByClickerPacket(npc, playerWhoAmI).Send();
			}
		}
	}
}
