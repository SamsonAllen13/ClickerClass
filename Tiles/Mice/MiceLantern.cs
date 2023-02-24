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
		public const int DrawOffsetY = -2;
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

			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.StyleWrapLimit = 111;
			TileObjectData.newTile.DrawYOffset = DrawOffsetY;
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
				int height = 16;
				spriteBatch.Draw(glowmaskAsset.Value.Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y + DrawOffsetY) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
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