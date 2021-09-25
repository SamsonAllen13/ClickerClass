using ClickerClass.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class Crystalized : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			LongerExpertDebuff = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			//TODO dire - Make sure this actually works in mutliplayer. It likely does not...
			//Even if it does, sometimes you get a double proc. Fixing that would help me a lot
			npc.GetGlobalNPC<ClickerGlobalNPC>().crystalSlime = true;
			if (npc.GetGlobalNPC<ClickerGlobalNPC>().crystalSlimeEnd && npc.buffTime[buffIndex] > 10)
			{
				npc.GetGlobalNPC<ClickerGlobalNPC>().crystalSlimeEnd = false;
				npc.buffTime[buffIndex] = 0;
			}
		}
	}
}
