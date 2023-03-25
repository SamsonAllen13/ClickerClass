using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace ClickerClass.Items.Consumables
{
	public class HeavenlyChip : ClickerItem
	{
		public static readonly int RadiusIncrease = 10;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RadiusIncrease);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.useTime = 45;
			Item.useAnimation = 45;
			Item.UseSound = SoundID.Item92;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.value = Item.sellPrice(0, 0, 40, 0);
			Item.rare = ItemRarityID.LightPurple;
		}

		public override bool CanUseItem(Player player)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			return !clickerPlayer.consumedHeavenlyChip;
		}

		public override bool? UseItem(Player player)
		{
			if (player.itemAnimation > 0 && player.ItemTimeIsZero)
			{
				ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				clickerPlayer.consumedHeavenlyChip = true;
				//No syncing required, click radius is not something that is seen or interacted with by other players
				return true;
			}

			return null;
		}
	}
}
