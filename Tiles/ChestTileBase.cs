using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ClickerClass.Tiles
{
	public abstract class ChestTileBase : ModTile
	{
		/// <summary>
		/// Has to be modded
		/// </summary>
		public abstract int PlacedItemType { get; }

		/// <summary>
		/// The key used to open this chest. -1 by default, which makes it not count as an unlockable chest
		/// </summary>
		public virtual int KeyItemType => -1;

		/// <summary>
		/// Chests naturally generating in the world (gold, shadow, but not dungeon ones) and lunar fragment chests have custom color
		/// </summary>
		public virtual Color MapColor => new Color(174, 129, 92);

		public bool IsUnlockable => KeyItemType > -1;

		public sealed override void SetStaticDefaults()
		{
			Main.tileSpelunker[Type] = true;
			Main.tileContainer[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;

			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.BasicChest[Type] = true;
			TileID.Sets.IsAContainer[Type] = true;
			TileID.Sets.AvoidedByNPCs[Type] = true;
			TileID.Sets.InteractibleByNPCs[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(Chest.FindEmptyChest, -1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(Chest.AfterPlacement_Hook, -1, 0, false);
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

			DustType = -1;
			AdjTiles = new int[] { TileID.Containers };

			AddMapEntry(MapColor, ItemLoader.GetItem(PlacedItemType).DisplayName, MapChestName);
			if (IsUnlockable)
			{
				AddMapEntry(MapColor, this.GetLocalization("MapEntry_Locked"), MapChestName);

				//Fallback for "used to be locked" and "locked" styles
				RegisterItemDrop(PlacedItemType, 1, 2);
			}

			SafeSetStaticDefaults();
		}

		public virtual void SafeSetStaticDefaults()
		{
			//tileShine(2), tileOreFinderPriority, new map entries, DustType
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
		{
			return true;
		}

		public override LocalizedText DefaultContainerName(int frameX, int frameY)
		{
			return Lang._mapLegendCache[MapHelper.TileToLookup(Type, IsUnlockable ? 0 : frameX / 36)];
		}

		public override ushort GetMapOption(int i, int j)
		{
			return (ushort)(IsLockedChest(i, j) ? 1 : 0);
		}

		public override bool IsLockedChest(int i, int j)
		{
			return IsUnlockable && Main.tile[i, j].TileFrameX / 36 == 2;
		}

		//UnlockChest overridden invididually

		public override bool LockChest(int i, int j, ref short frameXAdjustment, ref bool manual)
		{
			//We need to return true only if the tile style is the unlocked variant of a chest that supports locking.
			int style = Main.tile[i, j].TileFrameX / 36;
			if (style == 0)
			{
				frameXAdjustment += 36;
			}

			return IsUnlockable && style != 2;
		}

		public static string MapChestName(string name, int i, int j)
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
			int chest = Chest.FindChest(left, top);
			if (chest != -1 && Main.chest[chest].name != string.Empty)
			{
				name += ": " + Main.chest[chest].name;
			}
			return name;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Chest.DestroyChest(i, j);
		}

		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			Tile tile = Main.tile[i, j];
			Main.mouseRightRelease = false;
			int left = i;
			int top = j;
			if (tile.TileFrameX % 36 != 0)
			{
				left--;
			}
			if (tile.TileFrameY != 0)
			{
				top--;
			}

			player.CloseSign();
			player.SetTalkNPC(-1);
			Main.npcChatCornerItem = 0;
			Main.npcChatText = string.Empty;
			if (Main.editChest)
			{
				SoundEngine.PlaySound(SoundID.MenuTick);
				Main.editChest = false;
				Main.npcChatText = string.Empty;
			}
			if (player.editedChestName)
			{
				NetMessage.SendData(MessageID.SyncPlayerChest, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f);
				player.editedChestName = false;
			}

			bool isLocked = IsLockedChest(left, top);
			if (Main.netMode == NetmodeID.MultiplayerClient && !(IsUnlockable && isLocked))
			{
				if (left == player.chestX && top == player.chestY && player.chest != -1)
				{
					player.chest = -1;
					Recipe.FindRecipes();
					SoundEngine.PlaySound(SoundID.MenuClose);
				}
				else
				{
					NetMessage.SendData(MessageID.RequestChestOpen, -1, -1, null, left, top);
					Main.stackSplit = 600;
				}
			}
			else
			{
				if (IsUnlockable && isLocked)
				{
					int key = KeyItemType;
					if (player.ConsumeItem(key, includeVoidBag: true) && Chest.Unlock(left, top))
					{
						if (Main.netMode == NetmodeID.MultiplayerClient)
						{
							NetMessage.SendData(MessageID.LockAndUnlock, -1, -1, null, player.whoAmI, 1f, left, top);
						}
					}
				}
				else
				{
					int chest = Chest.FindChest(left, top);
					if (chest != -1)
					{
						Main.stackSplit = 600;
						if (chest == player.chest)
						{
							player.chest = -1;
							SoundEngine.PlaySound(SoundID.MenuClose);
						}
						else
						{
							SoundEngine.PlaySound(player.chest < 0 ? SoundID.MenuOpen : SoundID.MenuTick);
							player.OpenChest(left, top, chest);
						}

						Recipe.FindRecipes();
					}
				}
			}

			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			Tile tile = Main.tile[i, j];
			int left = i;
			int top = j;
			if (tile.TileFrameX % 36 != 0)
			{
				left--;
			}
			if (tile.TileFrameY != 0)
			{
				top--;
			}
			int chest = Chest.FindChest(left, top);
			player.cursorItemIconID = -1;
			if (chest < 0)
			{
				player.cursorItemIconText = Language.GetTextValue("LegacyChestType.0");
			}
			else
			{
				string defaultName = TileLoader.DefaultContainerName(tile.TileType, tile.TileFrameX, tile.TileFrameY); //This gets the ContainerName text for the currently selected language
				player.cursorItemIconText = Main.chest[chest].name.Length > 0 ? Main.chest[chest].name : defaultName;
				if (player.cursorItemIconText == defaultName)
				{
					player.cursorItemIconID = PlacedItemType;
					if (IsUnlockable && IsLockedChest(left, top))
					{
						player.cursorItemIconID = KeyItemType;
					}
					player.cursorItemIconText = string.Empty;
				}
			}
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
		}

		public override void MouseOverFar(int i, int j)
		{
			MouseOver(i, j);
			Player player = Main.LocalPlayer;
			if (player.cursorItemIconText == string.Empty)
			{
				player.cursorItemIconEnabled = false;
				player.cursorItemIconID = 0;
			}
		}
	}
}
