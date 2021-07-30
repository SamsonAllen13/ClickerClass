using ClickerClass.Items.Accessories;
using ClickerClass.Items.Weapons.Clickers;
using ClickerClass.Prefixes;
using ClickerClass.Utilities;
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

			ref var damage = ref player.GetDamage<ClickerDamage>();
			ref var crit = ref player.GetCritChance<ClickerDamage>();

			switch (item.prefix)
			{
				case PrefixID.Precise:
					crit += 2;
					break;
				case PrefixID.Lucky:
					crit += 4;
					break;
			}

			if (item.prefix == ModContent.PrefixType<ClickerRadius>())
			{
				clickerPlayer.clickerRadius += 0.3f;
			}

			switch (item.type)
			{
				case ItemID.Gi:
					crit += 5;
					break;
				case ItemID.CobaltBreastplate:
					crit += 3;
					break;
				case ItemID.PalladiumBreastplate:
					damage += 0.03f;
					crit += 2;
					break;
				case ItemID.PalladiumLeggings:
					damage += 0.02f;
					crit += 1;
					break;
				case ItemID.MythrilChainmail:
					damage += 0.05f;
					break;
				case ItemID.MythrilGreaves:
					crit += 3;
					break;
				case ItemID.OrichalcumBreastplate:
					crit += 6;
					break;
				case ItemID.AdamantiteBreastplate:
					damage += 0.06f;
					break;
				case ItemID.AdamantiteLeggings:
					crit += 4;
					break;
				case ItemID.TitaniumBreastplate:
					damage += 0.04f;
					crit += 3;
					break;
				case ItemID.TitaniumLeggings:
					damage += 0.03f;
					crit += 3;
					break;
				case ItemID.HallowedPlateMail:
					crit += 7;
					break;
				case ItemID.HallowedGreaves:
					damage += 0.07f;
					break;
				case ItemID.ChlorophytePlateMail:
					damage += 0.05f;
					crit += 7;
					break;
				case ItemID.ChlorophyteGreaves:
					crit += 8;
					break;
				case ItemID.DestroyerEmblem:
					crit += 8;
					break;
				case ItemID.EyeoftheGolem:
					crit += 10;
					break;
				case ItemID.PutridScent:
					crit += 5;
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
					crit += 2;
					break;
			}
		}

		//Tooltip stuff
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			// Clicker radius accessory prefix tooltip
			if (item.accessory && !item.social && item.prefix == ModContent.PrefixType<ClickerRadius>())
			{
				int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(Mod.Name))
				&& (tt.Name.Equals("Material") || tt.Name.StartsWith("Tooltip") || tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
				if (index != -1)
				{
					tooltips.Insert(index + 1, new TooltipLine(Mod, "PrefixAccClickerRadius", LangHelper.GetText("Prefix.ClickerRadius.Tooltip"))
					{
						isModifier = true
					});
				}
			}
		}
	}
}
