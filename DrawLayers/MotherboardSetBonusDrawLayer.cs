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

		private Asset<Texture2D> backTexture;
		private Asset<Texture2D> glowTexture;

		public override void Load()
		{
			if (!Main.dedServ)
			{
				backTexture = Mod.Assets.Request<Texture2D>("DrawLayers/MotherboardSetBonus_Back");
				glowTexture = Mod.Assets.Request<Texture2D>("DrawLayers/MotherboardSetBonus_Glow");
			}
		}

		public override void Unload()
		{
			backTexture = null;
			glowTexture = null;
		}

		public override bool GetDefaultVisiblity(PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();

			return modPlayer.CanDrawRadius && modPlayer.SetMotherboardDraw;
		}

		public override Position GetDefaultPosition()
		{
			return new AfterParent(PlayerDrawLayers.JimsCloak); //Furthest back layer
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			if (Main.gameMenu || drawInfo.shadow != 0f || drawPlayer.dead)
			{
				return;
			}

			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();

			float alpha = modPlayer.ClickerRadiusColorMultiplier;
			int drawX = (int)(drawPlayer.Center.X - Main.screenPosition.X);
			int drawY = (int)(drawPlayer.Center.Y + drawPlayer.gfxOffY - Main.screenPosition.Y);
			Vector2 center = new Vector2(drawX, drawY);
			Vector2 drawPos = center + modPlayer.CalculateMotherboardPosition(modPlayer.ClickerRadiusRealDraw).Floor();

			Texture2D texture = backTexture.Value;
			DrawData drawData = new DrawData(texture, drawPos, null, Color.White * alpha, 0f, texture.Size() / 2, 1f, SpriteEffects.None, 0)
			{
				ignorePlayerRotation = true
			};
			drawInfo.DrawDataCache.Add(drawData);

			texture = glowTexture.Value;
			Rectangle frame = texture.Frame(1, GlowFrameCount, frameY: modPlayer.setMotherboardFrame);
			drawData = new DrawData(texture, drawPos, frame, new Color(255, 255, 255, 100) * modPlayer.setMotherboardAlpha * alpha, 0f, new Vector2(texture.Width / 2, frame.Height / 2), 1f, SpriteEffects.None, 0)
			{
				ignorePlayerRotation = true
			};
			drawInfo.DrawDataCache.Add(drawData);
		}
	}
}
