using ClickerClass.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	/// <summary>
	/// Buff used to prevent the application of Crystalized for a short time
	/// </summary>
	public class CrystalizedFatigue : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			ClickerGlobalNPC clickerGlobalNPC = npc.GetGlobalNPC<ClickerGlobalNPC>();
			clickerGlobalNPC.crystalSlimeFatigue = true;
		}
	}
}
