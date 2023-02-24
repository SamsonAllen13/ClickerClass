using ClickerClass.Core.Netcode.Packets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ClickerClass.Tiles.Mice
{
	public class TrappedMiceChest : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSpelunker[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileOreFinderPriority[Type] = 500;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.AvoidedByNPCs[Type] = true;
			TileID.Sets.InteractibleByNPCs[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileID.Sets.BasicChestFake[Type] = true;
			TileID.Sets.IsATrigger[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
			TileObjectData.newTile.AnchorInvalidTiles = new int[]
			{
				TileID.MagicalIceBlock,
				TileID.Boulder,
				TileID.BouncyBoulder,
				TileID.LifeCrystalBoulder,
				TileID.RollingCactus
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);

			AdjTiles = new int[] { TileID.FakeContainers };
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
			AddMapEntry(new Color(172, 189, 246), CreateMapEntryName());
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

		public override ushort GetMapOption(int i, int j) => (ushort)(Main.tile[i, j].TileFrameX / 36);

		public override bool CreateDust(int i, int j, ref int type) => false;

		//TODO Drop might need to be re-added, TML doesn't handle the item drop for this tile

		public override bool RightClick(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			Main.mouseRightRelease = false;
			int left = i, top = j;
			if (tile.TileFrameX % 36 != 0)
			{
				left--;
			}
			if (tile.TileFrameY != 0)
			{
				top--;
			}

			Animation.NewTemporaryAnimation(2, tile.TileType, left, top);
			NetMessage.SendTemporaryAnimation(-1, 2, tile.TileType, left, top);

			Trigger(i, j);
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				new TriggerTrappedMiceChestPacket(left, top).Send();
			}

			return true;
		}

		public static void Trigger(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int left = i, top = j;
			if (tile.TileFrameX % 36 != 0)
			{
				left--;
			}
			if (tile.TileFrameY != 0)
			{
				top--;
			}
			SoundEngine.PlaySound(SoundID.Mech, new Vector2(i * 16, j * 16));
			Wiring.TripWire(left, top, 2, 2);
		}

		public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
		{
			Tile tile = Main.tile[i, j];
			int left = i, top = j;
			if (tile.TileFrameX % 36 != 0)
			{
				left--;
			}
			if (tile.TileFrameY != 0)
			{
				top--;
			}
			if (Animation.GetTemporaryFrame(left, top, out int newFrameYOffset))
			{
				frameYOffset = 38 * newFrameYOffset;
			}
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.cursorItemIconID = ModContent.ItemType<Items.Placeable.Mice.MiceChest_Trapped>();
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
		}
	}
}
