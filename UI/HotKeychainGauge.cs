using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using ReLogic.Content;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.GameContent;
using ClickerClass.Utilities;

namespace ClickerClass.UI
{
	internal class HotKeychainGauge : InterfaceResource
	{
		public HotKeychainGauge() : base("ClickerClass: Hot Keychain Gauge", InterfaceScaleType.UI) { }

		public const int MAX_FADE_TIME = 35;
		public const int FADE_DELAY = 5;
		public static int FadeTime { get; internal set; }
		private int _delay = 0;

		public override void Update(GameTime gameTime)
		{
			Player player = Main.LocalPlayer;
			ClickerPlayer clickerPlayer = Main.LocalPlayer.GetClickerPlayer();
			if (player.dead)
			{
				FadeTime = 0;
			}
			else if (clickerPlayer.accHotKeychain && !clickerPlayer.OutOfCombat)
			{
				FadeTime = MAX_FADE_TIME + FADE_DELAY;
				if (clickerPlayer.accHotKeychainAmount > 0)
				{
					_delay++;
				}
			}
			else if (FadeTime > 0)
			{
				FadeTime--;
				_delay = 0;
			}
		}

		protected override bool DrawSelf()
		{
			Player player = Main.LocalPlayer;

			// Don't draw if the options, the achievements or the key config menus are visible, if the player is a ghost or if the bar should be fully transparent
			if (player.dead || player.ghost || FadeTime == 0)
			{
				return true;
			}

			ClickerPlayer clickerPlayer = player.GetClickerPlayer();

			// Transparency Multiplier
			float alphaMult = Math.Min((float)FadeTime / MAX_FADE_TIME, 1);
			
			
			Asset<Texture2D> borderAsset;
			Texture2D borderTexture;
			borderAsset = ClickerClass.mod.Assets.Request<Texture2D>("UI/HotKeychainGauge_Sheet");

			if (!borderAsset.IsLoaded)
			{
				return true;
			}
			

			Texture2D texture = borderAsset.Value;
			Rectangle frame = texture.Frame(1, 3);
			Vector2 origin = frame.Size() / 2;

			// player.gfxOffY changes depending on if a player is moving on top of half or slanted blocks
			// Adding player.gfxOffY to the position calculation prevents position glitching
			Vector2 position = (player.Bottom + new Vector2(0, 14 + clickerPlayer.clickerGaugeOffset + player.gfxOffY)).Floor();
			Color color = Color.White * alphaMult;
			
			clickerPlayer.clickerGaugeOffset += 26;

			if (Main.playerInventory && Main.screenHeight < 1000)
			{
				if (player.breath < player.breathMax || player.lavaTime < player.lavaMax)
				{
					position.Y += 20 + 20 * ((player.breathMax - 1) / 200 + 1);
				}
			}
			// Calculates UI position depending on UI scale
			position = Vector2.Transform(position - Main.screenPosition, Main.GameViewMatrix.ZoomMatrix) / Main.UIScale;

			// Draw the darker background of the bar
			Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);

			// Percentage of bar filled
			float fill = (float)(clickerPlayer.accHotKeychainAmount) / 50;

			// Change the width of the frame so it only draws part of the bar
			frame.Width = (int)((frame.Width - 8) * fill + 8);
			frame.Y = frame.Height * 1;

			// Draw the filling of the bar
			Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);

			// Set the frame to the last one (Symbol), reset the width to normal, and then draw the symbol
			frame.Y = frame.Height * 2;
			frame.Width = texture.Width;
			Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);

			if (Main.ingameOptionsWindow || Main.InGameUI.IsVisible || Main.mouseText)
			{
				return true;
			}

			frame = new Rectangle((int)position.X - frame.Width / 2, (int)position.Y - frame.Height / 2, frame.Width, frame.Height);
			if (frame.Contains(Main.mouseX, Main.mouseY))
			{
				//player.showItemIcon = false;
				string text = "Heat: " + clickerPlayer.accHotKeychainAmount + " / 50";
				Main.instance.MouseTextHackZoom(text, Terraria.ID.ItemRarityID.Orange);
				Main.mouseText = true;
			}
			return true;
		}

		public override int GetInsertIndex(List<GameInterfaceLayer> layers)
		{
			return layers.FindIndex(layer => layer.Active && layer.Name.Equals("Vanilla: Ingame Options"));
		}
	}
}