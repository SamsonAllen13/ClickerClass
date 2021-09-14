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

			if (item.prefix == ModContent.PrefixType<ClickerRadius>())
			{
				clickerPlayer.clickerRadius += 0.3f;
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
