using ClickerClass.NPCs;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class Gouge : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Gouge");
			Description.SetDefault("Rapidly losing life");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Глубокий порез");
			Description.AddTranslation(GameCulture.Russian, "Стремительная потеря здоровья");
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
