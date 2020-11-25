using ClickerClass.NPCs;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class Embrittle : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Embrittle");
			Description.SetDefault("Clicks will deal 8 extra damage to this target");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Хрупкость");
			Description.AddTranslation(GameCulture.Russian, "Клики будут наносить дополнительно 8 единиц урона этой цели");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<ClickerGlobalNPC>().embrittle = true;
		}
	}
}
