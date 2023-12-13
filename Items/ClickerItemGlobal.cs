using ClickerClass.Items.Accessories;
using ClickerClass.Items.Weapons.Clickers;
using ClickerClass.Prefixes;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	public partial class ClickerItemGlobal : GlobalItem
	{
		//Specific to lock boxes, this is used to verify that we are on a suitable vanilla rule for rare items
		static bool CheckIfAtleastOneWithin(IItemDropRule[] rules, params int[] items)
		{
			foreach (var subEntry in rules)
			{
				if (subEntry is CommonDropNotScalingWithLuck rule && items.Contains(rule.itemId))
				{
					return true;
				}
				else if (subEntry is ItemDropWithConditionRule conditionRule && items.Contains(conditionRule.itemId))
				{
					return true;
				}
			}
			return false;
		}

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
						bool addedToVanilla = false;
						var ourRules = new IItemDropRule[]
						{
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<SlickClicker>())
						};

						foreach (var entry in itemLoot.Get(false))
						{
							if (entry is OneFromRulesRule lockboxRule)
							{
								if (CheckIfAtleastOneWithin(lockboxRule.options, ItemID.Valor, ItemID.Muramasa, ItemID.AquaScepter, ItemID.CobaltShield, ItemID.BlueMoon, ItemID.MagicMissile, ItemID.Handgun))
								{
									addedToVanilla = true;
									var set = new HashSet<IItemDropRule>(lockboxRule.options);
									set.UnionWith(ourRules);
									lockboxRule.options = set.ToArray();
									break;
								}
							}
						}

						if (!addedToVanilla)
						{
							//Fallback, halved chance to mirror intended drop rates. Change this if the amount of items in ourRules is increased!
							itemLoot.Add(new OneFromRulesRule(8, ourRules));
						}
					}
					break;
				case ItemID.ObsidianLockbox:
					{
						bool addedToVanilla = false;
						var ourRules = new IItemDropRule[]
						{
							ItemDropRule.NotScalingWithLuck(ModContent.ItemType<UmbralClicker>())
						};

						foreach (var entry in itemLoot.Get(false))
						{
							if (entry is OneFromRulesRule obsidianLockboxRule)
							{
								if (CheckIfAtleastOneWithin(obsidianLockboxRule.options, ItemID.DarkLance, ItemID.UnholyTrident, ItemID.Sunfury, ItemID.Flamelash, ItemID.HellwingBow))
								{
									addedToVanilla = true;
									var set = new HashSet<IItemDropRule>(obsidianLockboxRule.options);
									set.UnionWith(ourRules);
									obsidianLockboxRule.options = set.ToArray();
									break;
								}
							}
						}

						if (!addedToVanilla)
						{
							//Fallback, halved chance to mirror intended drop rates. Change this if the amount of items in ourRules is increased!
							itemLoot.Add(new OneFromRulesRule(6, ourRules));
						}
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
