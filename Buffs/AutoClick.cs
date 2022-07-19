using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class AutoClick : ModBuff
	{
		public static readonly AutoReuseEffect autoReuseEffect = new(6f, PreventsClickEffects: true);

		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<ClickerPlayer>().SetAutoReuseEffect(autoReuseEffect);
		}
	}
}
