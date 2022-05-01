using ClickerClass.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class Crystalized : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;

			//Required so NPC.RequestBuffRemoval works
			BuffID.Sets.CanBeRemovedByNetMessage[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			ClickerGlobalNPC clickerGlobalNPC = npc.GetGlobalNPC<ClickerGlobalNPC>();
			clickerGlobalNPC.crystalSlime = true;
		}
	}
}
