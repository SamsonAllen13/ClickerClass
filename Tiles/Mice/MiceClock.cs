using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ClickerClass.Tiles.Mice
{
	public class MiceClock : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.Clock[Type] = true;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.Height = 5;
			TileObjectData.newTile.Origin = new Point16(0, 4);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16 };
			TileObjectData.addTile(Type);
			
			AdjTiles = new int[] { TileID.GrandfatherClocks };
			AddMapEntry(new Color(191, 142, 111), Lang.GetItemName(ItemID.GrandfatherClock));
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

		public override bool CreateDust(int i, int j, ref int type) => false;

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 32, 80, ModContent.ItemType<Items.Placeable.Mice.MiceClock>());
		}

		public override bool RightClick(int x, int y)
		{
			string text = "AM";
			//Get current weird time
			double time = Main.time;
			if (!Main.dayTime)
			{
				//if it's night add this number
				time += 54000.0;
			}
			//Divide by seconds in a day * 24
			time = time / 86400.0 * 24.0;
			//Dunno why we're taking 19.5. Something about hour formatting
			time = time - 7.5 - 12.0;
			//Format in readable time
			if (time < 0.0)
			{
				time += 24.0;
			}
			if (time >= 12.0)
			{
				text = "PM";
			}
			int intTime = (int)time;
			//Get the decimal points of time.
			double deltaTime = time - intTime;
			//multiply them by 60. Minutes, probably
			deltaTime = (int)(deltaTime * 60.0);
			//This could easily be replaced by deltaTime.ToString()
			string text2 = string.Concat(deltaTime);
			if (deltaTime < 10.0)
			{
				//if deltaTime is eg "1" (which would cause time to display as HH:M instead of HH:MM)
				text2 = "0" + text2;
			}
			if (intTime > 12)
			{
				//This is for AM/PM time rather than 24hour time
				intTime -= 12;
			}
			if (intTime == 0)
			{
				//0AM = 12AM
				intTime = 12;
			}
			//Whack it all together to get a HH:MM format
			var newText = string.Concat("Time: ", intTime, ":", text2, " ", text);
			Main.NewText(newText, 255, 240, 20);
			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			Tile tile = Main.tile[i, j];
			int style = tile.TileFrameX / 36;
			player.cursorItemIconID = ModContent.ItemType<Items.Placeable.Mice.MiceClock>();
			player.cursorItemIconText = "";
			player.cursorItemIconEnabled = true;
		}
	}
}