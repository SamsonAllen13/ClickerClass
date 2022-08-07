using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ClickerClass.DrawLayers
{
	public class AimbotDrawLayer : PlayerDrawLayer
	{
		private Lazy<Asset<Texture2D>> aimbotTexture;
		private Lazy<Asset<Texture2D>> aimbotTexture2;

		public override void Load()
		{
			if (!Main.dedServ)
			{
				aimbotTexture = new(() => ModContent.Request<Texture2D>("ClickerClass/DrawLayers/AimbotModule_Glow"));
				aimbotTexture2 = new(() => ModContent.Request<Texture2D>("ClickerClass/DrawLayers/AimbotModule2_Glow"));
			}
		}

		public override void Unload()
		{
			aimbotTexture = null;
			aimbotTexture2 = null;
		}

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			if (drawPlayer.dead)
			{
				return false;
			}

			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();
			return modPlayer.HasAimbotModuleTarget;
		}

		public override Position GetDefaultPosition()
		{
			return new Between(PlayerDrawLayers.JimsCloak, PlayerDrawLayers.MountBack); //This decouples parenting relations, adds somewhere to the back of the player
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;

			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();
			NPC target = Main.npc[modPlayer.accAimbotModuleTarget];
			Vector2 drawPos = target.Center + new Vector2(0f, target.gfxOffY) - Main.screenPosition;

			Texture2D texture = aimbotTexture.Value.Value;
			texture = modPlayer.accAimbotModule2 ? texture : aimbotTexture2.Value.Value;
			float scale = modPlayer.accAimbotModuleScale;
			float alpha = 2f - (1f * scale);
			alpha *= modPlayer.accAimbotModuleTargetInRange ? 1f : 0.5f;
			DrawData drawData = new DrawData(texture, drawPos, null, new Color(255, 255, 255, 100) * alpha, 0f, texture.Size() / 2, scale, SpriteEffects.None, 0)
			{
				ignorePlayerRotation = true
			};
			drawInfo.DrawDataCache.Add(drawData);
		}
	}
}
