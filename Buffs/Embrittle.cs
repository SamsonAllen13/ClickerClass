using ClickerClass.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class Embrittle : ModBuff
	{
		public static readonly int ExtraDamage = 8;

		public override LocalizedText Description => base.Description.WithFormatArgs(ExtraDamage);

		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<ClickerGlobalNPC>().embrittle = true;
		}
	}
}
