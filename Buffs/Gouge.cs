using ClickerClass.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class Gouge : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			LongerExpertDebuff = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<ClickerGlobalNPC>().gouge = true;
		}
	}
}
