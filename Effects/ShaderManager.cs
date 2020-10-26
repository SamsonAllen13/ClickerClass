using ClickerClass.Items;
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

		public static void LoadCircleEffectAdds()
		{
			On.Terraria.Main.DrawInfernoRings += DrawCircles;
		}

		private static void DrawCircles(On.Terraria.Main.orig_DrawInfernoRings orig, Main self)
		{
			//TODO new shader
			orig(self);

			//Only draws for local player

			Player drawPlayer = Main.LocalPlayer;
			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();

			if (Main.gameMenu) return;

			if (!drawPlayer.dead)
			{
				if (modPlayer.clickerSelected)
				{
					bool phaseCheck = false;
					if (drawPlayer.HeldItem.modItem is ClickerItem clickerItem && clickerItem.isClicker)
					{
						if (clickerItem.itemClickerEffect.Contains("Phase Reach"))
						{
							phaseCheck = true;
						}
					}

					if (!phaseCheck)
					{
						float glow = modPlayer.clickerInRange || modPlayer.clickerInRangeMech ? 0.6f : 0f;

						Color outer = modPlayer.clickerColor * (0.2f + glow);
						Vector2 position = new Vector2((int)drawPlayer.Center.X, (int)drawPlayer.Center.Y + drawPlayer.gfxOffY);
						Effect shader = ShaderManager.SetupCircleEffect(position, (int)modPlayer.ClickerRadiusReal, outer);
						ShaderManager.ApplyToScreenOnce(Main.spriteBatch, shader);

						if (modPlayer.clickerMechSet && modPlayer.clickerMechSetRatio > 0)
						{
							outer = modPlayer.clickerColor * (0.2f + glow);
							position += modPlayer.CalculateMechPosition();
							shader = ShaderManager.SetupCircleEffect(position, (int)modPlayer.ClickerRadiusMech, outer);
							ShaderManager.ApplyToScreenOnce(Main.spriteBatch, shader);
						}
					}
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

		public static void Load()
		{
			if (Main.netMode != NetmodeID.Server)
			{
				CircleEffect = ClickerClass.mod.GetEffect("Effects/CircleShader/Circle");
				LoadCircleEffectAdds();
			}
		}

		public static void Unload()
		{
			CircleEffect = null;
		}


	}
}
