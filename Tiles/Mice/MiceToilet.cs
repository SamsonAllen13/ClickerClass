using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ClickerClass.Tiles.Mice
{
	public class MiceToilet : ModTile
	{
		public const int NextStyleHeight = 40;
		
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.CanBeSatOnForNPCs[Type] = true;
			TileID.Sets.CanBeSatOnForPlayers[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
			TileObjectData.newTile.CoordinatePaddingFix = new Point16(0, 2);
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;

			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1); //facing right will use the second texture style
			TileObjectData.addTile(Type);

			AdjTiles = new int[] { TileID.Toilets };
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
			AddMapEntry(new Color(191, 142, 111), Language.GetText("MapObject.Toilet"));
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => settings.player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance);

		public override bool CreateDust(int i, int j, ref int type) => false;

		public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info)
		{
			//It is very important to know that this is called on both players and NPCs, so do not use Main.LocalPlayer for example, use info.restingEntity
			Tile tile = Framing.GetTileSafely(i, j);

			//info.directionOffset = info.restingEntity is Player ? 6 : 2; // Default to 6 for players, 2 for NPCs
			//info.visualOffset = Vector2.Zero; // Defaults to (0,0)

			info.TargetDirection = -1;
			if (tile.TileFrameX != 0)
			{
				info.TargetDirection = 1; //Facing right if sat down on the right alternate (added through addAlternate in SetStaticDefaults earlier)
			}

			//The anchor represents the bottom-most tile of the chair. This is used to align the entity hitbox
			//Since i and j may be from any coordinate of the chair, we need to adjust the anchor based on that
			info.AnchorTilePosition.X = i; //Our chair is only 1 wide, so nothing special required
			info.AnchorTilePosition.Y = j;

			if (tile.TileFrameY % NextStyleHeight == 0)
			{
				info.AnchorTilePosition.Y++; //Here, since the chair is only 2 tiles high, we can just check if the tile is the top-most one, then move it 1 down
			}
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
			int item = ModContent.ItemType<Items.Placeable.Mice.MiceToilet>();
			if (item > 0)
			{
				player.cursorItemIconID = item;
			}

			if (tile.TileFrameX / 18 < 1)
			{
				player.cursorItemIconReversed = true;
			}
		}

		public override void HitWire(int i, int j) {
			// Spawn the toilet effect here when triggered by a signal
			Tile tile = Main.tile[i, j];

			int spawnX = i;
			int spawnY = j - (tile.TileFrameY % NextStyleHeight) / 18;

			Wiring.SkipWire(spawnX, spawnY);
			Wiring.SkipWire(spawnX, spawnY + 1);

			if (Wiring.CheckMech(spawnX, spawnY, 60)) {
				Projectile.NewProjectile(Wiring.GetProjectileSource(spawnX, spawnY), spawnX * 16 + 8, spawnY * 16 + 12, 0f, 0f, ProjectileID.ToiletEffect, 0, 0f, Main.myPlayer);
			}
		}
	}
}