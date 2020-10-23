using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using ClickerClass.Items;

namespace ClickerClass.UI
{
	internal class ClickerCursor : InterfaceResource
	{
		public ClickerCursor() : base("ClickerClass: Clicker", InterfaceScaleType.UI) { }

		private float _clickerScale = 0f;
		private float _clickerAlpha = 0f;
		private static bool _lastMouseInterface = false;

		/// <summary>
		/// Helper method that determines when the cursor can be drawn/replaced
		/// </summary>
		public static bool CanDrawCursor(Item item)
		{
			return !_lastMouseInterface && item.modItem is ClickerItem clickerItem && clickerItem.isClicker && item.damage > 0;
		}

		public override void Update(GameTime gameTime)
		{
			_clickerScale = Main.cursorScale;
			// For some reason cursorAlpha is "flipped", revert it here via some maths (0.8 is the value it fluctuates around)
			float flipped = 2 * 0.8f - Main.cursorAlpha;
			_clickerAlpha = flipped * 0.3f + 0.7f;
			// To safely cache when the cursor is inside an interface (directly accessing it when adding the cursor will not work because the vanilla logic hasn't reached that stage yet)
			_lastMouseInterface = Main.LocalPlayer.mouseInterface;
		}
		
		protected override bool DrawSelf()
		{
			Player player = Main.LocalPlayer;

			// Don't draw if the player is dead or a ghost
			if (player.dead || player.ghost || !ClickerConfigClient.Instance.ShowCustomCursors)
			{
				return true;
			}

			Texture2D borderTexture = ClickerClass.mod.GetTexture("UI/CursorOutline");
			Texture2D texture;
			Item item = player.HeldItem;
			if (CanDrawCursor(item))
			{
				texture = Main.itemTexture[item.type];
			}
			else
			{
				return true;
			}
			
			Rectangle borderFrame = borderTexture.Frame(1, 1);
			Vector2 borderOrigin = borderFrame.Size() / 2;
			
			Rectangle frame = texture.Frame(1, 1);
			Vector2 origin = frame.Size() / 2;

			Vector2 borderPosition = Main.MouseScreen;
			// Actual cursor is not drawn in the top left of the border but a bit offset, have to add/substract origins here
			Vector2 position = Main.MouseScreen - origin + borderOrigin;

			Color color = Color.White;
			color.A = (byte)(_clickerAlpha * 255);

			// Draw cursor border
			Main.spriteBatch.Draw(borderTexture, borderPosition, borderFrame, Main.mouseBorderColorSlider.GetColor(), 0f, Vector2.Zero, _clickerScale, SpriteEffects.FlipHorizontally, 0f);
			// Actual cursor
			Main.spriteBatch.Draw(texture, position, frame, color, 0f, Vector2.Zero, _clickerScale, SpriteEffects.FlipHorizontally, 0f);
			
			return true;
		}

		public override int GetInsertIndex(List<GameInterfaceLayer> layers)
		{
			return layers.FindIndex(layer => layer.Active && layer.Name.Equals("Vanilla: Cursor"));
		}
	}
}