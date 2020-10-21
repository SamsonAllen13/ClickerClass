using ClickerClass.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class Frozen : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Frozen");
			Description.SetDefault("Frozen in place");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<ClickerGlobalNPC>().frozen = true;
		}
	}
}
