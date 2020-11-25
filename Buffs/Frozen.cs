using ClickerClass.NPCs;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class Frozen : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Frozen");
			Description.SetDefault("Frozen in place");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Заморозка");
			Description.AddTranslation(GameCulture.Russian, "В замороженном состоянии");
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
