using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace ClickerClass.Tiles.Mice
{
	public class MiceLantern : ModTile
	{
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

			TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.HangingLanterns, 0));
			TileObjectData.addTile(Type);

			AdjTiles = new int[] { TileID.Torches };
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
			AddMapEntry(new Color(251, 235, 127), Language.GetText("MapObject.Lantern"));
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

		//Needed for AnchorType.PlatformNonHammered, adjust the numbers after % aaccordingly
		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
		{
			Tile tile = Main.tile[i, j];
			TileObjectData data = TileObjectData.GetTileData(tile);
			int topLeftX = i - tile.TileFrameX / 18 % data.Width;
			int topLeftY = j - tile.TileFrameY / 18 % data.Height;
			if (WorldGen.IsBelowANonHammeredPlatform(topLeftX, topLeftY))
			{
				offsetY -= 8;
			}
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			if (tile.TileFrameX < 18)
			{
				Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
				if (Main.drawToScreen)
				{
					zero = Vector2.Zero;
				}

				short frameX = tile.TileFrameX;
				short frameY = tile.TileFrameY;
				int width = 16;
				int offsetY = 0;
				int height = 16;
				TileLoader.SetDrawPositions(i, j, ref width, ref offsetY, ref height, ref frameX, ref frameY);
				spriteBatch.Draw(glowmaskAsset.Value.Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y + offsetY) + zero, new Rectangle(frameX, frameY, width, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
			}
		}

		public override void HitWire(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int width = 1;
			int height = 2;
			int x = i - tile.TileFrameX / 18 % width;
			int y = j - tile.TileFrameY / 18 % height;

			for (int l = x; l < x + width; l++)
			{
				for (int m = y; m < y + height; m++)
				{
					Tile checkTile = Framing.GetTileSafely(l, m);
					if (checkTile.HasTile && checkTile.TileType == Type)
					{
						if (checkTile.TileFrameX != 36)
						{
							checkTile.TileFrameX += 18;
						}
						if (checkTile.TileFrameX >= 36)
						{
							checkTile.TileFrameX -= 36;
						}
					}

					if (Wiring.running)
					{
						Wiring.SkipWire(l, m);
					}
				}
			}

			int w2 = width / 2;
			int h2 = height / 2;
			NetMessage.SendTileSquare(-1, x + w2 - 1, y + h2, 1 + w2 + h2);
		}
	}
}