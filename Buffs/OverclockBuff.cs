using Terraria;
using Terraria.ModLoader;
using ClickerClass.Utilities;

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
			player.GetModPlayer<ClickerPlayer>().clickerDamage -= 0.5f;
		}
	}
}
