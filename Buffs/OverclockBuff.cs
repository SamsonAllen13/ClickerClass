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
			player.GetDamage<ClickerDamage>() -= 0.5f;
		}
	}
}
