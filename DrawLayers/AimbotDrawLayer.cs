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
		private Asset<Texture2D> aimbotTexture;
		private Asset<Texture2D> aimbotTexture2;

		public override void Load()
		{
			if (!Main.dedServ)
			{
				aimbotTexture = Mod.Assets.Request<Texture2D>("DrawLayers/AimbotModule_Glow");
				aimbotTexture2 = Mod.Assets.Request<Texture2D>("DrawLayers/AimbotModule2_Glow");
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
			return modPlayer.accAimbotModuleTarget != -1 && modPlayer.accAimbotModuleFailsafe >= 10;
		}

		public override Position GetDefaultPosition()
		{
			return new AfterParent(PlayerDrawLayers.Leggings);
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;

			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();
			NPC target = Main.npc[modPlayer.accAimbotModuleTarget];
			Vector2 drawPos = target.Center + new Vector2(0f, target.gfxOffY) - Main.screenPosition;

			Texture2D texture = aimbotTexture.Value;
			texture = modPlayer.accAimbotModule2 ? texture : aimbotTexture2.Value;
			float percentage = modPlayer.accAimbotModuleScale;
			DrawData drawData = new DrawData(texture, drawPos, null, new Color(255, 255, 255, 100) * (2f - (1f * percentage)), 0f, texture.Size() / 2, percentage, SpriteEffects.None, 0)
			{
				ignorePlayerRotation = true
			};
			drawInfo.DrawDataCache.Add(drawData);
		}
	}
}
