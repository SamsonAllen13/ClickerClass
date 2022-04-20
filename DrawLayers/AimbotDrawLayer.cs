﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ClickerClass.DrawLayers
{
	public class AimbotDrawLayer : PlayerDrawLayer
	{
		private Asset<Texture2D> aimbotTexture;

		public override void Load()
		{
			if (!Main.dedServ)
			{
				aimbotTexture = Mod.Assets.Request<Texture2D>("DrawLayers/AimbotModule_Glow");
			}
		}

		public override void Unload()
		{
			aimbotTexture = null;
		}

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			if (drawPlayer.dead)
			{
				return false;
			}

			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();
			return modPlayer.accAimbotModuleTarget != -1 && modPlayer.accAimbotModuleFailsafe >= 10;
		}

		public override Position GetDefaultPosition()
		{
			return new BeforeParent(PlayerDrawLayers.JimsCloak); //Furthest back layer
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;

			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();
			NPC target = Main.npc[modPlayer.accAimbotModuleTarget];
			Vector2 drawPos = target.Center + new Vector2(0f, target.gfxOffY) - Main.screenPosition;

			Texture2D texture = aimbotTexture.Value;
			DrawData drawData = new DrawData(texture, drawPos, null, new Color(255, 255, 255, 50) * 0.65f, 0f, texture.Size() / 2, 1f, SpriteEffects.None, 0)
			{
				ignorePlayerRotation = true
			};
			drawInfo.DrawDataCache.Add(drawData);
		}
	}
}