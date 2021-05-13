using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Core.Netcode.Packets
{
	public class MouseResetTimeoutPacket : PlayerPacket
	{
		//For reflection
		public MouseResetTimeoutPacket() { }

		public MouseResetTimeoutPacket(Player player) : base(player)
		{

		}

		protected override void PostReceive(BinaryReader reader, int sender, Player player)
		{
			MousePlayer mousePlayer = player.GetModPlayer<MousePlayer>();
			mousePlayer.ResetTimeout();

			if (Main.netMode == NetmodeID.Server)
			{
				new MouseResetTimeoutPacket(player).Send(from: sender, bcCondition: delegate (Player otherPlayer)
				{
					//Only send to other player if the mouse would be in visible range
					Rectangle otherPlayerBounds = Utils.CenteredRectangle(otherPlayer.Center, MousePlayer.MaxRange);

					Vector2? _mouseWorld = mousePlayer.GetMousePosition();
					if (_mouseWorld is Vector2 mouseWorld)
					{
						Point mousePoint = mouseWorld.ToPoint();
						return otherPlayerBounds.Contains(mousePoint);
					}
					return false;
				});
			}
		}

		protected override void PostSend(BinaryWriter writer, Player player)
		{
			//Nothing
		}
	}
}
