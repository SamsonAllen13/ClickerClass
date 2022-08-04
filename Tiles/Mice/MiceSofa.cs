using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ClickerClass.Tiles.Mice
{
	public class MiceSofa : ModTile
	{
		public const int NextStyleHeight = 38;
		public const int NextStyleWidth = 54;

		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.CanBeSatOnForPlayers[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(Type);

			AdjTiles = new int[] { TileID.Benches };
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
			AddMapEntry(new Color(191, 142, 111), Lang.GetItemName(ItemID.Bench));
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => settings.player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance);

		public override bool CreateDust(int i, int j, ref int type) => false;

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 48, 32, ModContent.ItemType<Items.Placeable.Mice.MiceSofa>());
		}

		public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info)
		{
			Tile tile = Framing.GetTileSafely(i, j);

			info.TargetDirection = info.RestingEntity.direction;
			info.DirectionOffset = 0;
			Vector2 leftOffset = new Vector2(-4f, 2f);
			Vector2 rightOffset = new Vector2(4f, 2f);
			Vector2 centerOffset = new Vector2(0f, 2f);
			centerOffset.Y = leftOffset.Y = rightOffset.Y = 1f;

			Vector2 bonusOffset = Vector2.Zero;
			bonusOffset.X = 1f;

			info.FinalOffset.X = -1f;

			info.AnchorTilePosition.X = i;
			info.AnchorTilePosition.Y = j;

			if (tile.TileFrameY % NextStyleHeight == 0)
			{
				info.AnchorTilePosition.Y++;
			}

			if ((tile.TileFrameX % NextStyleWidth == 0 && info.TargetDirection == -1) || (tile.TileFrameX % NextStyleWidth == 36 && info.TargetDirection == 1))
			{
				info.VisualOffset = leftOffset;
			}
			else if ((tile.TileFrameX % NextStyleWidth == 0 && info.TargetDirection == 1) || (tile.TileFrameX % NextStyleWidth == 36 && info.TargetDirection == -1))
			{
				info.VisualOffset = rightOffset;
			}
			else
			{
				info.VisualOffset = centerOffset;
			}

			info.VisualOffset += bonusOffset;
		}

		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;

			if (player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance))
			{
				//Avoid being able to trigger it from long range
				player.GamepadEnableGrappleCooldown();
				player.sitting.SitDown(player, i, j);
			}

			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;

			if (!player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance))
			{
				//Match condition in RightClick. Interaction should only show if clicking it does something
				return;
			}

			player.noThrow = 2;
			player.cursorItemIconEnabled = true;

			Tile tile = Main.tile[i, j];
			int style = tile.TileFrameY / NextStyleHeight;
			int item = ModContent.ItemType<Items.Placeable.Mice.MiceSofa>();
			if (item > 0)
			{
				player.cursorItemIconID = item;
			}
		}
	}
}