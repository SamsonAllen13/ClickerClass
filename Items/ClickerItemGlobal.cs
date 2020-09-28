using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ClickerClass.Items;
using ClickerClass.NPCs;

namespace ClickerClass.Items
{
	public partial class ClickerItemGlobal : GlobalItem
	{
		// Add items to vanilla loot bags
		public override void OpenVanillaBag(string context, Player player, int arg)
		{
			switch (context)
			{
				case "bossBag":
					switch (arg)
					{
						case ItemID.MoonLordBossBag:
							player.QuickSpawnItem(ModContent.ItemType<LordsClicker>());
							if (Main.rand.NextBool(5))
							{
								player.QuickSpawnItem(ModContent.ItemType<TheClicker>());
							}
							break;
						case ItemID.WallOfFleshBossBag:
							if (Main.rand.NextBool(4))
							{
								player.QuickSpawnItem(ModContent.ItemType<ClickerEmblem>());
							}
							break;
					}
					break;
			}
		}
		
		public override void UpdateEquip(Item item, Player player)
		{
			switch (item.type)
			{
				case ItemID.Gi:
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 5;
					break;
				case ItemID.CobaltBreastplate:
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 3;
					break;
				case ItemID.PalladiumBreastplate:
					player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.03f;
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 2;
					break;
				case ItemID.PalladiumLeggings:
					player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.02f;
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 1;
					break;
				case ItemID.MythrilChainmail:
					player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.05f;
					break;
				case ItemID.MythrilGreaves:
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 3;
					break;
				case ItemID.OrichalcumBreastplate:
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 6;
					break;
				case ItemID.AdamantiteBreastplate:
					player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.06f;
					break;
				case ItemID.AdamantiteLeggings:
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 4;
					break;
				case ItemID.TitaniumBreastplate:
					player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.04f;
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 3;
					break;
				case ItemID.TitaniumLeggings:
					player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.03f;
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 3;
					break;
				case ItemID.HallowedPlateMail:
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 7;
					break;
				case ItemID.HallowedGreaves:
					player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.07f;
					break;
				case ItemID.ChlorophytePlateMail:
					player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.05f;
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 7;
					break;
				case ItemID.ChlorophyteGreaves:
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 8;
					break;
				case ItemID.DestroyerEmblem:
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 8;
					break;
				case ItemID.EyeoftheGolem:
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 10;
					break;
				case ItemID.PutridScent:
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 5;
					break;
				case ItemID.SunStone:
					if (Main.dayTime)
					{
						goto case ItemID.CelestialShell;
					}
					break;
				case ItemID.MoonStone:
					if (!Main.dayTime || Main.eclipse)
					{
						goto case ItemID.CelestialShell;
					}
					break;
				case ItemID.CelestialStone:
				case ItemID.CelestialShell:
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 2;
					break;
			}
		}
	}
}