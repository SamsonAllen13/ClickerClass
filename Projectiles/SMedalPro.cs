using Terraria;

namespace ClickerClass.Projectiles
{
	public class SMedalPro : MedalProBase
	{
		public override void AI()
		{
			base.AI();

			Player player = Main.player[Projectile.owner];
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();

			if (clickerPlayer.AccSMedal)
			{
				Projectile.timeLeft = 10;
			}
		}
	}
}
