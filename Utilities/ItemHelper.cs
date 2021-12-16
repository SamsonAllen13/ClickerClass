using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ClickerClass.Utilities
{
	internal static class ItemHelper
	{
		/// <summary>
		/// Draws a basic single-frame glowmask for an item dropped in the world. Use in <see cref="Terraria.ModLoader.ModItem.PostDrawInWorld"/>
		/// </summary>
		public static void BasicInWorldGlowmask(this Item item, SpriteBatch spriteBatch, Texture2D glowTexture, Color color, float rotation, float scale)
		{
			spriteBatch.Draw(
				glowTexture,
				new Vector2(
					item.position.X - Main.screenPosition.X + item.width * 0.5f,
					item.position.Y - Main.screenPosition.Y + item.height - glowTexture.Height * 0.5f
				),
				new Rectangle(0, 0, glowTexture.Width, glowTexture.Height),
				color,
				rotation,
				glowTexture.Size() * 0.5f,
				scale,
				SpriteEffects.None,
				0f);
		}
	}
}
