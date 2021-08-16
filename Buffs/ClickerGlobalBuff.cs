using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class ClickerGlobalBuff : GlobalBuff
	{
		public override void Update(int type, Player player, ref int buffIndex)
		{
			ref var crit = ref player.GetCritChance<ClickerDamage>();
			switch (type)
			{
				case BuffID.WellFed:
					crit += 2;
					break;

				case BuffID.Rage:
					crit += 10;
					break;
			}
		}
	}
}
