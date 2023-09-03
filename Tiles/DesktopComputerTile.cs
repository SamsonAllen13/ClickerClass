using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using ClickerClass.Buffs;

using ReLogic.Content;
using System;
using Terraria.DataStructures;

namespace ClickerClass.Tiles
{
	public class DesktopComputerTile : ModTile
	{
		public const int DrawOffsetY = 2;
		public static Lazy<Asset<Texture2D>> glowmaskAsset;

		public override void Load()
		{
			if (!Main.dedServ)
			{
				glowmaskAsset = new(() => ModContent.Request<Texture2D>(Texture + "_Glow"));
			}
		}

		public override void Unload()
		{
			glowmaskAsset = null;
		}
		
		public override void SetStaticDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
			TileObjectData.addTile(Type);
			AddMapEntry(new Color(125, 180, 230), ModContent.GetInstance<Items.Placeable.DesktopComputer>().DisplayName);
			DustType = 1;
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
		{
			return true;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 15 : 3;
		}
		
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.1f;
			g = 0.2f;
			b = 0.3f;
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			int height = 16;
			spriteBatch.Draw(glowmaskAsset.Value.Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y + DrawOffsetY) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), Color.White * 0.5f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
		
		}

		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			SoundEngine.PlaySound(SoundID.Camera);
			player.AddBuff(ModContent.BuffType<DesktopComputerBuff>(), 30 * 60 * 60);
			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;

			player.cursorItemIconID = ModContent.ItemType<Items.Placeable.DesktopComputer>();
			player.cursorItemIconText = "";

			player.cursorItemIconEnabled = true;
		}
	}
}