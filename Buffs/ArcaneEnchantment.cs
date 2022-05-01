using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class ArcaneEnchantment : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.accEnlarge = true;
			player.GetDamage<ClickerDamage>() += 0.15f;
		}
	}
}
