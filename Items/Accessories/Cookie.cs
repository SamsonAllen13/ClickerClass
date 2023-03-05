using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ClickerClass.Items.Consumables;

namespace ClickerClass.Items.Accessories
{
	public class Cookie : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<HeavenlyChip>();
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 0, 40, 0);
			Item.rare = ItemRarityID.Blue;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.accCookieItem = Item;
			clickerPlayer.accCookie = true;
		}
	}
}
