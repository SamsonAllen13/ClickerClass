using Terraria;

namespace ClickerClass.Projectiles
{
	public class FMedalPro : MedalProBase
	{
		public override void AI()
		{
			base.AI();

			Player player = Main.player[Projectile.owner];
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();

			if (clickerPlayer.AccFMedal)
			{
				Projectile.timeLeft = 10;
			}
		}
	}
}
