using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class DesktopComputerBuff : ModBuff
	{
		public static readonly int ClickAmountDecrease = 10;
		
		public override LocalizedText Description => base.Description.WithFormatArgs(ClickAmountDecrease);
		
		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			BuffID.Sets.TimeLeftDoesNotDecrease[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerBonusPercent -= ClickAmountDecrease / 100f;
		}
	}
}
