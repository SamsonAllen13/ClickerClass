using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
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

		public override bool GetDefaultVisiblity(PlayerDrawSet drawInfo)
		{
			return true;
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			if (drawInfo.shadow != 0f || drawPlayer.dead || drawPlayer.frozen || drawPlayer.itemAnimation <= 0)
			{
				return;
			}

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

			switch (useStyle)
			{
				//TODO only support for the melee swing usestyle for now
				case 0:
					break;
				case 1:
					Texture2D weaponGlow = data.Texture.Value;
					Vector2 position = new Vector2((int)(drawInfo.ItemLocation.X - Main.screenPosition.X), (int)(drawInfo.ItemLocation.Y - Main.screenPosition.Y));
					Vector2 origin = new Vector2(drawPlayer.direction == -1 ? weaponGlow.Width : 0, drawPlayer.gravDir == -1 ? 0 : weaponGlow.Height);
					DrawData drawData = new DrawData(weaponGlow, position, null, data.Color, drawPlayer.itemRotation, origin, heldItem.scale, drawInfo.itemEffect, 0);
					drawInfo.DrawDataCache.Add(drawData);
					break;
				default:
					break;
			}
		}
	}
}
