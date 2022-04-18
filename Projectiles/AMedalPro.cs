using Terraria;

namespace ClickerClass.Projectiles
{
	public class AMedalPro : MedalProBase
	{
		public override void AI()
		{
			base.AI();

			Player player = Main.player[Projectile.owner];
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();

			if (clickerPlayer.AccAMedal)
			{
				Projectile.timeLeft = 10;
			}
		}
	}
}
