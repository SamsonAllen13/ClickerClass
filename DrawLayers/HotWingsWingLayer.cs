using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ClickerClass.DrawLayers
{
	public class HotWingsWingLayer : PlayerDrawLayer
	{
		private Asset<Texture2D> wingTexture;

		public override void Load()
		{
			if (!Main.dedServ)
			{
				wingTexture = Mod.Assets.Request<Texture2D>("DrawLayers/HotWings_Wings");
			}
		}

		public override void Unload()
		{
			wingTexture = null;
		}

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			if (drawPlayer.dead)
			{
				return false;
			}

			return drawPlayer.GetModPlayer<ClickerPlayer>().DrawHotWings;
		}

		public override Position GetDefaultPosition()
		{
			return new Between(PlayerDrawLayers.MountBack, PlayerDrawLayers.Wings); //MountBack is one of the first layers, we want it before Wings to match order roughly
			//Not using XParent because that would make it a child, affected by visibility hierarchy, which we don't want as we hide the Wings layer while this is visible
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			//Mostly copied from vanilla
			Player drawPlayer = drawInfo.drawPlayer;

			Asset<Texture2D> asset = wingTexture;
			Texture2D texture = asset.Value;

			Vector2 directions = drawPlayer.Directions;
			Vector2 offset = new Vector2(0f, 7f);
			Vector2 position = drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height / 2) + offset;

			int num11 = 0;
			int num12 = 0;
			ClickerPlayer clickerPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();
			int numFrames = ClickerPlayer.EffectHotWingsFrameMax;

			float fade = 1f;

			const int fadeStart = ClickerPlayer.EffectHotWingsTimerFadeStart;
			int timer = clickerPlayer.effectHotWingsTimer;
			if (timer < fadeStart)
			{
				fade = (float)timer / fadeStart;
			}

			Color mainColor = Color.Lerp(drawInfo.colorArmorBody, Color.White * fade, 0.4f);

			Color color = /*drawInfo.colorArmorBody*/ drawPlayer.GetImmuneAlpha(mainColor, drawInfo.shadow);

			position += new Vector2(num12 - 9, num11 + 2) * directions;
			position = position.Floor();
			Rectangle frame = new Rectangle(0, asset.Height() / numFrames * clickerPlayer.effectHotWingsFrame, asset.Width(), asset.Height() / numFrames);
			DrawData data = new DrawData(texture, position.Floor(), frame, color, drawPlayer.bodyRotation, new Vector2(asset.Width() / 2, asset.Height() / numFrames / 2), 1f, drawInfo.playerEffect, 0);
			//data.shader = drawInfo.cWings;
			drawInfo.DrawDataCache.Add(data);
		}
	}
}
