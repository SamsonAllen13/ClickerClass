using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class ClickerGlobalBuff : GlobalBuff
	{
		public override void Update(int type, Player player, ref int buffIndex)
		{
			switch (type)
			{
				case BuffID.WellFed:
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 2;
					break;

				case BuffID.Rage:
					player.GetModPlayer<ClickerPlayer>().clickerCrit += 10;
					break;
			}
		}
	}
}
