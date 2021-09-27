using ClickerClass.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class Stunned : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			LongerExpertDebuff = true;

			ClickerClass.BossBuffImmunity.Add(Type);
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<ClickerGlobalNPC>().stunned = true;
		}
	}
}
