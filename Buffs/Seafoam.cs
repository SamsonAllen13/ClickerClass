using ClickerClass.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class Seafoam : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;

			ClickerClass.BossBuffImmunity.Add(Type);
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetClickerGlobalNPC().seafoam = true;
		}
	}
}
