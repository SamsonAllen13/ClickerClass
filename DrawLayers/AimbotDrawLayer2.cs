using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ClickerClass.DrawLayers
{
	//TODO - Very redudant, should be reduced down to one .cs file but I ran into an issue:
	//Only issue was the fade-in of the scaling was off center when a second frame was introduced
	public class AimbotDrawLayer2 : PlayerDrawLayer
	{
		private Asset<Texture2D> aimbotTexture;

		public override void Load()
		{
			if (!Main.dedServ)
			{
				aimbotTexture = Mod.Assets.Request<Texture2D>("DrawLayers/AimbotModule2_Glow");
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
			return !modPlayer.accAimbotModule2 && modPlayer.accAimbotModuleTarget != -1 && modPlayer.accAimbotModuleFailsafe >= 10;
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
			float percentage = modPlayer.accAimbotModuleScale;
			DrawData drawData = new DrawData(texture, drawPos, null, new Color(255, 255, 255, 100) * (2f - (1f * percentage)), 0f, texture.Size() / 2, percentage, SpriteEffects.None, 0)
			{
				ignorePlayerRotation = true
			};
			drawInfo.DrawDataCache.Add(drawData);
		}
	}
}
