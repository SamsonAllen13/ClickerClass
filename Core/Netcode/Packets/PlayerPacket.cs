using System.IO;
using Terraria;

namespace ClickerClass.Core.Netcode.Packets
{
	public abstract class PlayerPacket : MPPacket
	{
		protected readonly byte whoAmI;

		//For reflection
		public PlayerPacket() { }

		public PlayerPacket(Player player)
		{
			whoAmI = (byte)player.whoAmI;
		}

		public sealed override void Send(BinaryWriter writer)
		{
			writer.Write((byte)whoAmI);
			PostSend(writer, Main.player[whoAmI]);
		}

		protected abstract void PostSend(BinaryWriter writer, Player player);

		public sealed override void Receive(BinaryReader reader, int sender)
		{
			byte whoAmI = reader.ReadByte();
			PostReceive(reader, sender, Main.player[whoAmI]);
		}

		protected abstract void PostReceive(BinaryReader reader, int sender, Player player);
	}
}
