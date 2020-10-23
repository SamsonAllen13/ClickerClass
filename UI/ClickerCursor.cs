using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using ClickerClass.Items;
using ClickerClass.Utilities;

namespace ClickerClass.UI
{
	internal class ClickerCursor : InterfaceResource
	{
		public ClickerCursor() : base("ClickerClass: Clicker", InterfaceScaleType.UI) { }

		private float _clickerScale = 0f;
		private bool _clickerScaleShift = false;
		
		public override void Update(GameTime gameTime)
		{
			_clickerScale += !_clickerScaleShift ? 0.005f : -0.005f;

			if (_clickerScale > 0.09f)
			{
				_clickerScaleShift = true;
			}
			else if (_clickerScale < -0.09f)
			{
				_clickerScaleShift = false;
			}
		}
		
		protected override bool DrawSelf()
		{
			Player player = Main.LocalPlayer;
			ClickerPlayer clickerPlayer = player.GetClickerPlayer();

			// Don't draw if the player is dead or a ghost
			if (player.dead || player.ghost || !ClickerConfigClient.Instance.ShowCustomCursors)
			{
				return true;
			}

			Texture2D texture = ClickerClass.mod.GetTexture("UI/CursorOutline");
			Texture2D texture2 = ClickerClass.mod.GetTexture("UI/CursorOutline");
			if (player.HeldItem.modItem is ClickerItem clickerItem && clickerItem.isClicker && player.HeldItem.damage > 0)
			{
				texture2 = Main.itemTexture[player.HeldItem.type];
			}
			else
			{
				return true;
			}
			
			Rectangle frame = texture.Frame(1, 1);
			Vector2 origin = frame.Size() / 2;
			
			Rectangle frame2 = texture2.Frame(1, 1);
			Vector2 origin2 = frame2.Size() / 2;

			Vector2 position = new Vector2(Main.mouseX + 8, Main.mouseY + 11);
			Vector2 position2 = new Vector2(Main.mouseX + 8, Main.mouseY + 11);
			Color color = Color.White;

			// Draw cursor border
			Main.spriteBatch.Draw(texture, position, frame, Main.mouseBorderColorSlider.GetColor(), 0f, origin, 1f + _clickerScale, SpriteEffects.FlipHorizontally, 0f);
			Main.spriteBatch.Draw(texture2, position2, frame2, color, 0f, origin2, 1f + _clickerScale, SpriteEffects.FlipHorizontally, 0f);
			
			return true;
		}

		public override int GetInsertIndex(List<GameInterfaceLayer> layers)
		{
			return layers.FindIndex(layer => layer.Active && layer.Name.Equals("Vanilla: Cursor"));
		}
	}
}