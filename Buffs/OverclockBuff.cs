using ClickerClass.Items.Armors;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class OverclockBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerBonusPercent -= 0.5f;
			player.GetDamage<ClickerDamage>() -= OverclockHelmet.SetBonusDamageDecrease / 100f;
		}
	}
}
