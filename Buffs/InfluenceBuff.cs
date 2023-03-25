using ClickerClass.Items.Consumables;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class InfluenceBuff : ModBuff
	{
		public static readonly int RadiusIncrease = InfluencePotion.RadiusIncrease;

		public override LocalizedText Description => base.Description.WithFormatArgs(RadiusIncrease);

		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<ClickerPlayer>().clickerRadius += 2 * RadiusIncrease / 100f;
		}
	}
}
