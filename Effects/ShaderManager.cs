using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Effects
{
	/// <summary>
	/// Responsible for dealing with shaders
	/// </summary>
	public static class ShaderManager
	{
		public static Effect CircleEffect { get; private set; }

		public static Effect SetupCircleEffect(Vector2 center, int radius, Color edgeColor, Color bodyBolor = default)
		{
			Effect circle = CircleEffect;
			if (circle != null)
			{
				circle.Parameters["ScreenPos"].SetValue(Main.screenPosition);
				circle.Parameters["ScreenDim"].SetValue(new Vector2(Main.screenWidth, Main.screenHeight));
				circle.Parameters["EntCenter"].SetValue(center);
				circle.Parameters["EdgeColor"].SetValue(edgeColor.ToVector4());
				circle.Parameters["BodyColor"].SetValue((bodyBolor == default ? Color.Transparent : bodyBolor).ToVector4());
				circle.Parameters["Radius"].SetValue(radius);
				circle.Parameters["HpPercent"].SetValue(1f);
				float thickness = 2f;
				circle.Parameters["ShrinkResistScale"].SetValue(thickness / 24f);
			}
			return circle;
		}

		public static void ApplyToScreenOnce(SpriteBatch spriteBatch, Effect effect, bool restore = true)
		{
			if (effect == null) return;

			//Apply the shader to the spritebatch from now on
			StartEffectOnSpriteBatch(spriteBatch, effect);

			DrawEmptyCanvasToScreen(spriteBatch);

			//Normally you want to apply the shader once, and restore. This is for when you repeatedly apply a different effect without restoring vanilla
			if (restore)
			{
				//Stop applying the shader, continue normal behavior
				RestoreVanillaSpriteBatchSettings(spriteBatch);
			}
		}

		public static void StartEffectOnSpriteBatch(SpriteBatch spriteBatch, Effect effect)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.instance.Rasterizer, effect, Main.GameViewMatrix.TransformationMatrix);
		}

		public static void DrawEmptyCanvasToScreen(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Main.magicPixel, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Transparent);
		}

		public static void RestoreVanillaSpriteBatchSettings(SpriteBatch spriteBatch)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.instance.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
		}

		public static void Load()
		{
			if (Main.netMode != NetmodeID.Server)
			{
				CircleEffect = ClickerClass.mod.GetEffect("Effects/CircleShader/Circle");
			}
		}

		public static void Unload()
		{
			CircleEffect = null;
		}
	}
}
