using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public delegate void ExtraAction();

	public static class ProjectileExtras
	{
		public static void DrawChain(int index, Vector2 to, string chainPath, Func<Color, Color> colorOverride = null, float alphaPercent = 1f)
		{
			Texture2D texture = ModContent.Request<Texture2D>(chainPath).Value;
			Projectile projectile = Main.projectile[index];
			Vector2 position = projectile.Center;
			Rectangle? sourceRectangle = new Rectangle?();
			Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
			float num1 = (float)texture.Height;
			Vector2 vector2_4 = to - position;
			float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
			bool flag = true;
			if (float.IsNaN(position.X) && float.IsNaN(position.Y))
			{
				flag = false;
			}

			if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
			{
				flag = false;
			}

			while (flag)
			{
				if ((double)vector2_4.Length() < (double)num1 + 1.0)
				{
					flag = false;
				}
				else
				{
					Vector2 vector2_1 = vector2_4;
					vector2_1.Normalize();
					position += vector2_1 * num1;
					vector2_4 = to - position;
					Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
					color2 = projectile.GetAlpha(color2);
					if (colorOverride != null)
					{
						color2 = colorOverride(color2);
					}
					Main.EntitySpriteDraw(texture, position - Main.screenPosition, sourceRectangle, color2 * alphaPercent, rotation, origin, 1f, SpriteEffects.None, 0);
				}
			}
		}
	}
}
