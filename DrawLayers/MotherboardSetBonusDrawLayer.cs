using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ClickerClass.DrawLayers
{
	public class MotherboardSetBonusDrawLayer : PlayerDrawLayer
	{
		private const int GlowFrameCount = 4;

		private Lazy<Asset<Texture2D>> backTexture;
		private Lazy<Asset<Texture2D>> glowTexture;

		public override void Load()
		{
			if (!Main.dedServ)
			{
				backTexture = new(() => ModContent.Request<Texture2D>("ClickerClass/DrawLayers/MotherboardSetBonus_Back"));
				glowTexture = new(() => ModContent.Request<Texture2D>("ClickerClass/DrawLayers/MotherboardSetBonus_Glow"));
			}
		}

		public override void Unload()
		{
			backTexture = null;
			glowTexture = null;
		}

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;

			if (drawInfo.shadow != 0f)
			{
				//Gameplay only: no armor shadows
				return false;
			}

			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();

			return modPlayer.CanDrawRadius && modPlayer.SetMotherboardPlaced;
		}

		public override Position GetDefaultPosition()
		{
			return new BeforeParent(PlayerDrawLayers.JimsCloak); //Furthest back layer
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;

			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();

			float alpha = modPlayer.ClickerRadiusColorMultiplier;
			int drawX = (int)(drawPlayer.Center.X - Main.screenPosition.X);
			int drawY = (int)(drawPlayer.Center.Y + drawPlayer.gfxOffY - Main.screenPosition.Y);
			Vector2 center = new Vector2(drawX, drawY);
			Vector2 drawPos = center + modPlayer.CalculateMotherboardPosition(modPlayer.ClickerRadiusRealDraw).Floor();

			Texture2D texture = backTexture.Value.Value;
			DrawData drawData = new DrawData(texture, drawPos, null, Color.White * alpha, 0f, texture.Size() / 2, 1f, SpriteEffects.None, 0)
			{
				ignorePlayerRotation = true
			};
			drawInfo.DrawDataCache.Add(drawData);

			texture = glowTexture.Value.Value;
			Rectangle frame = texture.Frame(1, GlowFrameCount, frameY: modPlayer.setMotherboardFrame);
			drawData = new DrawData(texture, drawPos, frame, new Color(255, 255, 255, 100) * modPlayer.setMotherboardAlpha * alpha, 0f, new Vector2(texture.Width / 2, frame.Height / 2), 1f, SpriteEffects.None, 0)
			{
				ignorePlayerRotation = true
			};
			drawInfo.DrawDataCache.Add(drawData);
		}
	}
}
