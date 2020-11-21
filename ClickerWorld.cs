using ClickerClass.Items.Accessories;
using ClickerClass.Items.Weapons.Clickers;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace ClickerClass
{
	public class ClickerWorld : ModWorld
	{
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
			if (genIndex != -1)
			{
				// Extra Chest Loot
				tasks.Insert(++genIndex, new PassLegacy("Clicker Class: Extra Loot", GenerateExtraLoot));
			}
		}

		private void GenerateExtraLoot(GenerationProgress progress)
		{
			progress.Message = "Clicker Class: Generating Extra Loot";

			HashSet<ChestStyle> chestStyles = new HashSet<ChestStyle>
			{
				ChestStyle.Wooden, ChestStyle.Gold, ChestStyle.LockedGold, ChestStyle.RichMahogany,
				ChestStyle.Ivy, ChestStyle.LivingWood, ChestStyle.WebCovered, ChestStyle.Water,
				ChestStyle.Mushroom, ChestStyle.Granite, ChestStyle.Marble, ChestStyle.Lihzahrd,
				ChestStyle.LockedShadow
			};

			Dictionary<ChestStyle, List<Chest>> chestLists = new Dictionary<ChestStyle, List<Chest>>();

			for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
			{
				Chest chest = Main.chest[chestIndex];
				if (chest == null || !WorldGen.InWorld(chest.x, chest.y, 42)) // don't include chests generated outside the playable area of the map
				{
					continue;
				}

				Tile tile = Main.tile[chest.x, chest.y];
				if (tile.type != TileID.Containers || chest.item == null)
				{
					continue;
				}

				ChestStyle style = (ChestStyle)(tile.frameX / 36);
				if (chestStyles.Contains(style))
				{
					if (style == ChestStyle.LockedGold && !Main.wallDungeon[tile.wall]) // not actually a dungeon chest, maybe some mod added this
					{
						continue;
					}

					if (style == ChestStyle.Wooden && Main.wallDungeon[tile.wall]) // wooden chests generated inside the dungeon always have golden keys
					{
						continue;
					}

					if (style == ChestStyle.Mushroom || style == ChestStyle.Granite || style == ChestStyle.Marble) // consider all these as gold chests, since that's what vanilla does
					{
						style = ChestStyle.Gold;
					}

					if (!chestLists.ContainsKey(style))
					{
						chestLists[style] = new List<Chest>();
					}

					chestLists[style].Add(chest);
				}
			}

			if (chestLists.ContainsKey(ChestStyle.Gold))
			{
				ReplaceRareItemsInChests(chestLists[ChestStyle.Gold], new int[] { ModContent.ItemType<EnchantedLED>() });
			}

			if (chestLists.ContainsKey(ChestStyle.LockedGold))
			{
				ReplaceRareItemsInChests(chestLists[ChestStyle.LockedGold], new int[] { ModContent.ItemType<SlickClicker>() });
			}

			if (chestLists.ContainsKey(ChestStyle.LockedShadow))
			{
				ReplaceRareItemsInChests(chestLists[ChestStyle.LockedShadow], new int[] { ModContent.ItemType<UmbralClicker>() });
			}
		}

		private void ReplaceRareItemsInChests(IList<Chest> chestList, IList<int> itemsToPlaceInChests, int rareSlots = 1, Func<int, IList<Chest>> generateChestFunc = null)
		{
			Dictionary<int, List<Chest>> chestsWithItem = new Dictionary<int, List<Chest>>(); // A dictionary where the key is an item id, and the value is a list of chests the item is in
			List<Chest> availableChests = new List<Chest>(); // A list of chests we can place our items into

			int itemCount = itemsToPlaceInChests.Count;
			int itemChoice = 0;
			int slot = 0;

			for (int i = 0; i < chestList.Count; i++) // Loop through all chest in the list provided as a parameter
			{
				Chest chest = chestList[i];
				if (chest.item[slot] == null || chest.item[slot].IsAir)
				{
					continue;
				}

				if (!chestsWithItem.ContainsKey(chest.item[slot].type)) // check if the item in the slot already has an entry in the dictionary
				{
					chestsWithItem[chest.item[slot].type] = new List<Chest>(); // if not, create it
				}

				chestsWithItem[chest.item[slot].type].Add(chest); // then we add the chest to the list of chests we can find the item in
			}

			foreach (var list in chestsWithItem.Values) // then, we loop through all entries in the dictionary
			{
				list.RemoveAt(WorldGen.genRand.Next(list.Count)); // and remove one chest from the list of chest in the current entry at random
				if (list.Count > 0) // if the list still has chests left then an item generated in more than 1 chest
				{
					availableChests.AddRange(list); // so we add the chests left to the list of chests we can add items to
				}
			}

			int itemsInChestsCount = chestsWithItem.Keys.Count;

			// now comes the hard part

			if (availableChests.Count < itemCount) // If we got bad luck and didn't get enough chests to place our items into
			{
				int neededChests = itemCount - availableChests.Count;
				if (generateChestFunc != null) // we see if we provided a method to generate more chests
				{
					// if we did, we just generate enough to add our items
					IList<Chest> chests = generateChestFunc(neededChests);
					availableChests.AddRange(chests); // then we add the chests to our list
					for (int i = 0; i < chests.Count; i++)
					{
						chestList.Add(chests[i]); // and also add them to the list provided as a parameter
					}
				}
				else // if we didn't
				{
					availableChests = new List<Chest>(chestList); // we reset the list of available chests to the list of chest we were provided at the beginning
					while (availableChests.Count > neededChests) // but remove chest at random until we only have enough chests to generate our items
					{
						availableChests.RemoveAt(WorldGen.genRand.Next(availableChests.Count));
					}

					for (int i = 0; i < availableChests.Count; i++) // then we loop through chests on the list
					{
						Chest chest = availableChests[i];
						for (int k = chest.item.Length - 1; k > slot + rareSlots; k--) // then through all items in the chest, except for the ones specified as rare
						{
							chest.item[k] = chest.item[k - 1]; // and make room for our item
						}

						chest.item[slot + rareSlots] = new Item();
						// this means that if for some reason a chest was full, the last item on the chest will be deleted... >:)
					}
					slot += rareSlots;
					rareSlots = 1;
				}
			}

			while (availableChests.Count > 0) // now we generate our items
			{
				int index = WorldGen.genRand.Next(availableChests.Count);
				Chest chest = availableChests[index]; // we chose a random chest on the list

				// we check if we already added at least one of each item we wanted to add
				if (itemChoice < itemCount || WorldGen.genRand.Next(itemsInChestsCount + itemCount) < itemCount) // if we did, we randomly generate a number to see if we can generate another one
				{
					int tempRareSlots = rareSlots;
					if (chest.item[slot].type == ItemID.FlareGun) // the flaregun is a special case, is the only item in vanilla that generates along with another item (flares) on a pool (gold chest) full of single rare items
					{
						tempRareSlots = 2;
					}

					while (tempRareSlots > 1) // if the amount of slots the rare items take is higher than 1
					{
						for (int i = slot + tempRareSlots - 1; i < chest.item.Length - 1; i++) // since we are replacing the rare items, we remove all of them
						{
							chest.item[i] = chest.item[i + 1];
						}

						chest.item[chest.item.Length - 1] = new Item();
						tempRareSlots--;
					}
					chest.item[slot].SetDefaults(itemChoice < itemCount ? itemsToPlaceInChests[itemChoice] : WorldGen.genRand.Next(itemsToPlaceInChests)); // we replace the item in the slot with ours
					chest.item[slot].Prefix(-1); // and prefix it for good measure
					itemChoice++;
				}
				availableChests.RemoveAt(index); // then we remove the current chest from the list
			}
			return; // note that this doesn't handle cases were the list of chests provided as a parameter is smaller than the amount of items to generate
		}
	}
}
