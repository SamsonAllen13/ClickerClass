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
							player.QuickSpawnItem(ModContent.ItemType<TheClicker>());
							break;
					}
					break;
			}
		}
	}
}