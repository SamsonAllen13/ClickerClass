using Terraria;
using Terraria.ModLoader;
using ClickerClass.NPCs;

namespace ClickerClass.Buffs
{
	public class Gouge : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Gouge");
			Description.SetDefault("Rapidly losing life");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<ClickerGlobalNPC>().gouge = true;
		}
	}
}
