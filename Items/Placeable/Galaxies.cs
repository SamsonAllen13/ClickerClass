using ClickerClass.Core.Netcode.Packets;
using ClickerClass.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Placeable
{
	//Sold by traveling merchant. If sold by any other town NPC, uncomment the code in MoonLordPaintingNPC.OnKill
	public class Galaxies : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Painting6x4>(), 0); //tile, style
			Item.width = 30;
			Item.height = 30;
			Item.value = Item.sellPrice(0, 0, 20, 0);
			Item.rare = ItemRarityID.White;
		}
	}
	
	//Separate GlobalNPC so no baggage with the tracking and logic on all other NPCs
	public class MoonLordPaintingNPC : GlobalNPC
	{
		//No syncing needed to other clients
		private HashSet<int> playersDamagedByClicker = new HashSet<int>();

		public override bool InstancePerEntity => true;

		public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
		{
			return entity.type == NPCID.MoonLordCore;
		}

		public override GlobalNPC Clone(NPC from, NPC to)
		{
			var clone = (MoonLordPaintingNPC)base.Clone(from, to);
			clone.playersDamagedByClicker = new HashSet<int>(from.GetGlobalNPC<MoonLordPaintingNPC>().playersDamagedByClicker);
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

		public override void OnKill(NPC npc)
		{
			foreach (var whoAmI in playersDamagedByClicker)
			{
				Player player = Main.player[whoAmI];
				if (!player.active)
				{
					continue;
				}

				player.GetModPlayer<ClickerPlayer>().paintingCondition_MoonLordDefeatedWithClicker = true;

				//This is only needed if the painting is sold by regular town NPC, and not traveling merchant
				//if (Main.netMode == NetmodeID.Server)
				//{
				//	//One packet per player, only to player
				//	new MoonLordDefeatedWithClickerPacket(player).Send(to: whoAmI);
				//}
			}
		}

		public static void SetDamagedByClicker(NPC npc, int playerWhoAmI)
		{
			if (!npc.TryGetGlobalNPC<MoonLordPaintingNPC>(out var globalNPC))
			{
				return;
			}

			//Only the client applying this needs to know (to avoid spamming packet)
			globalNPC.playersDamagedByClicker.Add(playerWhoAmI);

			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				new MoonLordDamagedByClickerPacket(npc, playerWhoAmI).Send();
			}
		}

		public static bool CanDropItemForPlayer(NPC npc, Player player)
		{
			if (!npc.TryGetGlobalNPC<MoonLordPaintingNPC>(out var globalNPC))
			{
				return false;
			}

			return globalNPC.playersDamagedByClicker.Contains(player.whoAmI);
		}
	}
}
