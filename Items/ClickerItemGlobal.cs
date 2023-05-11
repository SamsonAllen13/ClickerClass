using ClickerClass.Items.Accessories;
using ClickerClass.Items.Weapons.Clickers;
using ClickerClass.Prefixes;
using ClickerClass.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace ClickerClass.Items
{
	public partial class ClickerItemGlobal : GlobalItem
	{
		// Add items to vanilla loot bags
		public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
		{
			switch (item.type)
			{
				/* Crates */
				#region Crates
				case ItemID.FrozenCrate:
					{
						itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<IcePack>(), 6));
					}
					break;
				case ItemID.FrozenCrateHard: goto case ItemID.FrozenCrate;
				case ItemID.FloatingIslandFishingCrate:
					{
						itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StarryClicker>(), 3));
					}
					break;
				case ItemID.FloatingIslandFishingCrateHard: goto case ItemID.FloatingIslandFishingCrate;
				#endregion

				/* Lock Boxes */
				#region Lock Boxes
				case ItemID.LockBox:
					{
						itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SlickClicker>(), 7));
					}
					break;
				case ItemID.ObsidianLockbox:
					{
						itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<UmbralClicker>(), 6));
					}
					break;
				#endregion

				/* Treasure Bags */
				#region Treasure Bags
				case ItemID.KingSlimeBossBag:
					{
						itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StickyKeychain>(), 4));
					}
					break;
				case ItemID.DeerclopsBossBag:
					{
						itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CyclopsClicker>(), 4));
					}
					break;
				case ItemID.WallOfFleshBossBag:
					{
						itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BurningSuperDeathClicker>(), 4));
						itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ClickerEmblem>(), 4));
					}
					break;
				case ItemID.QueenSlimeBossBag:
					{
						itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ClearKeychain>(), 4));
					}
					break;
				case ItemID.TwinsBossBag:
					{
						itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BottomlessBoxofPaperclips>(), 4));
					}
					break;
				case ItemID.SkeletronPrimeBossBag: goto case ItemID.TwinsBossBag;
				case ItemID.DestroyerBossBag: goto case ItemID.TwinsBossBag;
				case ItemID.BossBagBetsy:
					{
						itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DraconicClicker>(), 4));
					}
					break;
				case ItemID.FishronBossBag:
					{
						itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SeafoamClicker>(), 5));
					}
					break;
				case ItemID.FairyQueenBossBag:
					{
						itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RainbowClicker>(), 4));
					}
					break;
				case ItemID.MoonLordBossBag:
					{
						itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<LordsClicker>()));
						itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheClicker>(), 5));
					}
					break;
				#endregion
			}
		}

		public override void UpdateEquip(Item item, Player player)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();

			ref var damage = ref player.GetDamage<ClickerDamage>();
			ref var crit = ref player.GetCritChance<ClickerDamage>();

			if (item.prefix == ModContent.PrefixType<ClickerRadius>())
			{
				clickerPlayer.clickerRadius += 2 * ClickerRadius.RadiusIncrease / 100f;
			}
		}
	}
}
