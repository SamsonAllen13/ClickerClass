using System.IO;
using Terraria;

namespace ClickerClass.Core.Netcode.Packets
{
	public class SyncClickerPlayerPacket : PlayerPacket
	{
		private readonly bool clickerAutoClick = false;
		//Add more fields here and to the ctor/write/read

		//For reflection
		public SyncClickerPlayerPacket() { }

		public SyncClickerPlayerPacket(ClickerPlayer clickerPlayer) : base(clickerPlayer.Player)
		{
			clickerAutoClick = clickerPlayer.clickerAutoClick;
		}

		protected override void PostSend(BinaryWriter writer, Player player)
		{
			writer.Write((bool)clickerAutoClick);
		}

		protected override void PostReceive(BinaryReader reader, int sender, Player player)
		{
			bool clickerAutoClick = reader.ReadBoolean();

			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerAutoClick = clickerAutoClick;
		}
	}
}
