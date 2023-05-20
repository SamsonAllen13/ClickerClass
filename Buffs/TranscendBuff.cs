using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Drawing;

namespace ClickerClass.Buffs
{
	public class TranscendBuff : ModBuff
	{
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
