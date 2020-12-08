using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class CookieBuff : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerRadius += 0.4f;
			clickerPlayer.clickerDamage += 0.1f;
			player.lifeRegen += 2;
		}
	}
}
