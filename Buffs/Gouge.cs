using ClickerClass.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class Gouge : ModBuff
	{
		public static readonly int DamageOverTime = 30;

		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetClickerGlobalNPC().gouge = true;
		}
	}
}
