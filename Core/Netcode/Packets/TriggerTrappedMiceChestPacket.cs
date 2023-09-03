using ClickerClass.Tiles.Mice;
using System.IO;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Core.Netcode.Packets
{
	public class TriggerTrappedChestPacket : MPPacket
	{
		protected readonly short left;
		protected readonly short top;

		//For reflection
		public TriggerTrappedChestPacket() { }

		public TriggerTrappedChestPacket(int left, int top)
		{
			this.left = (short)left;
			this.top = (short)top;
		}

		public override void Send(BinaryWriter writer)
		{
			writer.Write(left);
			writer.Write(top);
		}

		public sealed override void Receive(BinaryReader reader, int sender)
		{
			short left = reader.ReadInt16();
			short top = reader.ReadInt16();

			Wiring.SetCurrentUser(sender);
			TrappedMiceChest.Trigger(left, top);
			Wiring.SetCurrentUser(-1);

			if (Main.netMode == NetmodeID.Server)
			{
				new TriggerTrappedChestPacket(left, top).Send(-1, sender);
			}
		}
	}
}
