using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ClickerClass.Tiles.Mice
{
	public class MiceCandle : ModTile
	{
		public const int DrawOffsetY = -4;
		public static Lazy<Asset<Texture2D>> glowmaskAsset;

		public override void Load()
		{
			if (!Main.dedServ)
			{
				glowmaskAsset = new(() => ModContent.Request<Texture2D>(Texture + "_Glow"));
			}
		}

		public override void SetStaticDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.newTile.CoordinateHeights = new int[] { 20 };
			TileObjectData.newTile.DrawYOffset = DrawOffsetY;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.StyleLineSkip = 2;
			TileObjectData.addTile(Type);

			AdjTiles = new int[] { TileID.Torches };
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
			AddMapEntry(new Color(253, 221, 3), Lang.GetItemName(ItemID.Candle));
		}

		public override bool CreateDust(int i, int j, ref int type) => false;

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			Tile tile = Main.tile[i, j];
			if (tile.TileFrameX < 18)
			{
				r = 0.7f;
				g = 0.75f;
				b = 1f;
			}
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			if (!TileDrawing.IsVisible(tile))
			{
				return;
			}

			if (tile.TileFrameX < 18)
			{
				Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
				if (Main.drawToScreen)
				{
					zero = Vector2.Zero;
				}
				int height = tile.TileFrameY == 36 ? 18 : 16;
				spriteBatch.Draw(glowmaskAsset.Value.Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y + DrawOffsetY) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
			}
		}

		public override void HitWire(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int x = i;
			int y = j;
			if (tile.TileFrameX != 36)
			{
				tile.TileFrameX += 18;
			}
			if (tile.TileFrameX >= 36)
			{
				tile.TileFrameX -= 36;
			}

			if (Wiring.running)
			{
				Wiring.SkipWire(x, y);
			}

			NetMessage.SendTileSquare(-1, x, y, 1);
		}
	}
}