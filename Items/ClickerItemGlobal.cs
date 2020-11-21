using ClickerClass.Items.Accessories;
using ClickerClass.Items.Weapons.Clickers;
using ClickerClass.Prefixes;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
						case ItemID.KingSlimeBossBag:
							if (Main.rand.NextBool(4))
							{
								player.QuickSpawnItem(ModContent.ItemType<StickyKeychain>());
							}
							break;
					}
					break;
			}
		}

		public override void UpdateEquip(Item item, Player player)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			switch (item.prefix)
			{
				case PrefixID.Precise:
					clickerPlayer.clickerCrit += 2;
					break;
				case PrefixID.Lucky:
					clickerPlayer.clickerCrit += 4;
					break;
			}

			if (item.prefix == ModContent.PrefixType<ClickerRadius>())
			{
				clickerPlayer.clickerRadius += 0.3f;
			}

			switch (item.type)
			{
				case ItemID.Gi:
					clickerPlayer.clickerCrit += 5;
					break;
				case ItemID.CobaltBreastplate:
					clickerPlayer.clickerCrit += 3;
					break;
				case ItemID.PalladiumBreastplate:
					clickerPlayer.clickerDamage += 0.03f;
					clickerPlayer.clickerCrit += 2;
					break;
				case ItemID.PalladiumLeggings:
					clickerPlayer.clickerDamage += 0.02f;
					clickerPlayer.clickerCrit += 1;
					break;
				case ItemID.MythrilChainmail:
					clickerPlayer.clickerDamage += 0.05f;
					break;
				case ItemID.MythrilGreaves:
					clickerPlayer.clickerCrit += 3;
					break;
				case ItemID.OrichalcumBreastplate:
					clickerPlayer.clickerCrit += 6;
					break;
				case ItemID.AdamantiteBreastplate:
					clickerPlayer.clickerDamage += 0.06f;
					break;
				case ItemID.AdamantiteLeggings:
					clickerPlayer.clickerCrit += 4;
					break;
				case ItemID.TitaniumBreastplate:
					clickerPlayer.clickerDamage += 0.04f;
					clickerPlayer.clickerCrit += 3;
					break;
				case ItemID.TitaniumLeggings:
					clickerPlayer.clickerDamage += 0.03f;
					clickerPlayer.clickerCrit += 3;
					break;
				case ItemID.HallowedPlateMail:
					clickerPlayer.clickerCrit += 7;
					break;
				case ItemID.HallowedGreaves:
					clickerPlayer.clickerDamage += 0.07f;
					break;
				case ItemID.ChlorophytePlateMail:
					clickerPlayer.clickerDamage += 0.05f;
					clickerPlayer.clickerCrit += 7;
					break;
				case ItemID.ChlorophyteGreaves:
					clickerPlayer.clickerCrit += 8;
					break;
				case ItemID.DestroyerEmblem:
					clickerPlayer.clickerCrit += 8;
					break;
				case ItemID.EyeoftheGolem:
					clickerPlayer.clickerCrit += 10;
					break;
				case ItemID.PutridScent:
					clickerPlayer.clickerCrit += 5;
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
					clickerPlayer.clickerCrit += 2;
					break;
			}
		}

		//Tooltip stuff
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			// Clicker radius accessory prefix tooltip
			if (item.accessory && !item.social && item.prefix == ModContent.PrefixType<ClickerRadius>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(mod.Name))
				&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(mod, "PrefixAccClickerRadius", "+15% base clicker radius")
					{
						isModifier = true
					});
				}
			}
		}
	}
}
