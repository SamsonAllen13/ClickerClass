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
		#region Circle Effect
		public static Effect CircleEffect { get; private set; }

		/// <summary>
		/// Prepares the CircleEffect shader with proper values. If radius2 is 0, the second circle won't be drawn
		/// </summary>
		public static Effect SetupCircleEffect(Vector2 center, int radius, Color edgeColor, float thick = 2f, Color bodyColor = default, Vector2 center2 = default, int radius2 = 0)
		{
			Effect circle = CircleEffect;
			if (circle != null)
			{
				circle.Parameters["ScreenPos"].SetValue(Main.screenPosition);
				circle.Parameters["ScreenDim"].SetValue(new Vector2(Main.screenWidth, Main.screenHeight));
				circle.Parameters["EdgeColor"].SetValue(edgeColor.ToVector4());
				circle.Parameters["BodyColor"].SetValue((bodyColor == default ? Color.Transparent : bodyColor).ToVector4());
				circle.Parameters["Thickness"].SetValue(thick);

				//circle.Parameters["Center1"].SetValue(position);
				//circle.Parameters["Radius1"].SetValue(100);

				//circle.Parameters["SecondCircle"].SetValue(true);
				//circle.Parameters["Center2"].SetValue(Main.MouseWorld);
				//circle.Parameters["Radius2"].SetValue(50);

				//2 is the current size the shader accepts
				Vector2[] centers = new Vector2[2] { center, center2 };
				float[] radii = new float[2] { radius, radius2 };

				circle.Parameters["Centers"].SetValue(centers);
				circle.Parameters["Radii"].SetValue(radii);
			}
			return circle;
		}

		internal static void LoadCircleEffectAdds()
		{
			On.Terraria.Main.DrawInfernoRings += DrawCircles;
		}

		private static void DrawCircles(On.Terraria.Main.orig_DrawInfernoRings orig, Main self)
		{
			orig(self);

			//Only draws for local player

			Player drawPlayer = Main.LocalPlayer;

			if (Main.gameMenu) return;

			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();

			if (!drawPlayer.dead && modPlayer.clickerSelected)
			{
				if (modPlayer.clickerDrawRadius)
				{
					float glow = modPlayer.GlowVisual ? 0.6f : 0f;

					Color outer = modPlayer.clickerRadiusColor * (0.2f + glow);


					Vector2 center = new Vector2((int)drawPlayer.Center.X, (int)drawPlayer.Center.Y + drawPlayer.gfxOffY);
					Vector2 motherboardCenter = default;
					int radius = (int)modPlayer.ClickerRadiusReal;
					int motherboardRadius = 0;

					if (modPlayer.SetMotherboardDraw)
					{
						//Don't use clickerMotherboardSetPosition here as it includes the wrong player.Center
						motherboardCenter = center + modPlayer.CalculateMotherboardPosition().Floor();
						motherboardRadius = (int)modPlayer.ClickerRadiusMotherboard;
					}

					Effect shader = SetupCircleEffect(center, radius, outer, center2: motherboardCenter, radius2: motherboardRadius);
					ApplyToScreenOnce(Main.spriteBatch, shader);
				}
			}
		}
		#endregion

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

		internal static void Load()
		{
			if (Main.netMode != NetmodeID.Server)
			{
				CircleEffect = ClickerClass.mod.GetEffect("Effects/CircleShader/Circle");
				LoadCircleEffectAdds();
			}
		}

		internal static void Unload()
		{
			CircleEffect = null;
		}
	}
}
