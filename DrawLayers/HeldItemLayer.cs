using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ClickerClass.DrawLayers
{
	//Items manually register data which this layer is using
	public sealed class HeldItemLayer : PlayerDrawLayer
	{
		private static Dictionary<(int type, int useStyle), DrawLayerData> ItemLayerData { get; set; }

		/// <summary>
		/// Add data associated with the item type (key) here, usually in <see cref="ModType.SetStaticDefaults"/>.
		/// <para>Don't forget the !Main.dedServ check!</para>
		/// </summary>
		/// <param name="type">Item type</param>
		/// <param name="data">Data</param>
		/// <param name="useStyle">Decides what useStyle this data associates with. -1 for defaulting current held items useStyle.
		/// <para>This is important if an item switches between useStyles (right click for example), then you register multiple draw layers</para></param>
		public static void RegisterData(int type, DrawLayerData data, int useStyle = -1)
		{
			var tuple = new ValueTuple<int, int>(type, useStyle);
			if (!ItemLayerData.ContainsKey(tuple))
			{
				ItemLayerData.Add(tuple, data);
			}
		}

		public override void Load()
		{
			ItemLayerData = new Dictionary<(int, int), DrawLayerData>();
		}

		public override void Unload()
		{
			ItemLayerData = null;
		}

		public override Position GetDefaultPosition()
		{
			return new AfterParent(PlayerDrawLayers.HeldItem);
		}

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			Item heldItem = drawInfo.heldItem;
			bool usingItem = drawPlayer.itemAnimation > 0 && heldItem.useStyle != 0;
			bool holdingSuitableItem = heldItem.holdStyle != 0 && !drawPlayer.pulley;
			if (!drawPlayer.CanVisuallyHoldItem(heldItem))
			{
				holdingSuitableItem = false;
			}

			if (drawInfo.shadow != 0f || drawPlayer.JustDroppedAnItem || drawPlayer.frozen || !(usingItem || holdingSuitableItem) || heldItem.type <= 0 || drawPlayer.dead || heldItem.noUseGraphic || drawPlayer.wet && heldItem.noWet || drawPlayer.happyFunTorchTime && drawPlayer.HeldItem.createTile == TileID.Torches && drawPlayer.itemAnimation == 0)
			{
				return false;
			}

			return true;
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;

			Item heldItem = drawInfo.heldItem;
			int useStyle = heldItem.useStyle;

			DrawLayerData data = null;
			foreach (var pair in ItemLayerData)
			{
				var tuple = pair.Key;
				var value = pair.Value;
				if (tuple.type == heldItem.type)
				{
					if (tuple.useStyle == useStyle)
					{
						data = value; //If found matching useStyle, successful
						break;
					}
					else if (tuple.useStyle == -1)
					{
						data = value; //Keep track of fallback
					}
				}
			}

			if (data == null)
			{
				return;
			}

			Texture2D weaponGlow = data.Texture.Value;
			float adjustedItemScale = drawPlayer.GetAdjustedItemScale(heldItem);
			Vector2 position = new Vector2((int)(drawInfo.ItemLocation.X - Main.screenPosition.X), (int)(drawInfo.ItemLocation.Y - Main.screenPosition.Y));
			Rectangle? sourceRect = new Rectangle(0, 0, weaponGlow.Width, weaponGlow.Height);

			//TODO only support for some use styles for now
			if (useStyle == ItemUseStyleID.Swing)
			{
				Vector2 origin = new Vector2(drawPlayer.direction == -1 ? weaponGlow.Width : 0, drawPlayer.gravDir == -1 ? 0 : weaponGlow.Height);
				Color color = data.Color(drawInfo);
				DrawData drawData = new DrawData(weaponGlow, position, sourceRect, color, drawPlayer.itemRotation, origin, adjustedItemScale, drawInfo.itemEffect, 0);
				drawInfo.DrawDataCache.Add(drawData);
			}
			else if (useStyle == ItemUseStyleID.Shoot)
			{
				Color color = data.Color(drawInfo);
				if (Item.staff[heldItem.type])
				{
					float num9 = drawInfo.drawPlayer.itemRotation + 0.785f * (float)drawInfo.drawPlayer.direction;
					float num10 = 0f;
					float num11 = 0f;
					Vector2 originStaff = new Vector2(0f, weaponGlow.Height);

					if (drawInfo.drawPlayer.gravDir == -1f)
					{
						if (drawInfo.drawPlayer.direction == -1)
						{
							num9 += 1.57f;
							originStaff = new Vector2(weaponGlow.Width, 0f);
							num10 -= weaponGlow.Width;
						}
						else
						{
							num9 -= 1.57f;
							originStaff = Vector2.Zero;
						}
					}
					else if (drawInfo.drawPlayer.direction == -1)
					{
						originStaff = new Vector2(weaponGlow.Width, weaponGlow.Height);
						num10 -= weaponGlow.Width;
					}

					ItemLoader.HoldoutOrigin(drawInfo.drawPlayer, ref originStaff);

					DrawData drawDataStaff = new DrawData(weaponGlow, new Vector2((int)(drawInfo.ItemLocation.X - Main.screenPosition.X + originStaff.X + num10), (int)(drawInfo.ItemLocation.Y - Main.screenPosition.Y + num11)), sourceRect, color, num9, originStaff, adjustedItemScale, drawInfo.itemEffect, 0);
					drawInfo.DrawDataCache.Add(drawDataStaff);

					return;
				}

				Vector2 vector5 = new Vector2(weaponGlow.Width / 2, weaponGlow.Height / 2);
				Vector2 vector6 = Main.DrawPlayerItemPos(drawPlayer.gravDir, heldItem.type);
				int num12 = (int)vector6.X;
				vector5.Y = vector6.Y;
				Vector2 origin = new Vector2(-num12, weaponGlow.Height / 2);
				if (drawPlayer.direction == -1)
				{
					origin = new Vector2(weaponGlow.Width + num12, weaponGlow.Height / 2);
				}

				DrawData drawData = new DrawData(weaponGlow, new Vector2((int)(drawInfo.ItemLocation.X - Main.screenPosition.X + vector5.X), (int)(drawInfo.ItemLocation.Y - Main.screenPosition.Y + vector5.Y)), sourceRect, color, drawPlayer.itemRotation, origin, adjustedItemScale, drawInfo.itemEffect, 0);
				drawInfo.DrawDataCache.Add(drawData);
			}
		}
	}
}
