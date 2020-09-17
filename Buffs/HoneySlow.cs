using Terraria;
using Terraria.ModLoader;
using ClickerClass.NPCs;

namespace ClickerClass.Buffs
{
	public class HoneySlow : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Honey");
			Description.SetDefault("Movement speed significantly reduced");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<ClickerGlobalNPC>().honeySlow = true;
		}
	}
}
