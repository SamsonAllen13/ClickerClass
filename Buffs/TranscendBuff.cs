using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class TranscendBuff : ModBuff
	{
		public static readonly int LifeHealedPercent = 10;

		public override LocalizedText Description => base.Description.WithFormatArgs(LifeHealedPercent);

		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<ClickerPlayer>().effectTranscend = true;

			if (Main.rand.NextBool(18))
			{
				ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.PrincessWeapon, new ParticleOrchestraSettings
				{
					PositionInWorld = player.Center,
					MovementVector = Main.rand.NextVector2Circular(3f, 3f)
				}, player.whoAmI);
			}
		}
	}
}
