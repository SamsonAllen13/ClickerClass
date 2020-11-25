using ClickerClass.NPCs;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class HoneySlow : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Honey");
			Description.SetDefault("Movement speed significantly reduced");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Мёд");
			Description.AddTranslation(GameCulture.Russian, "Скорость передвижения значительно снижена");
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
