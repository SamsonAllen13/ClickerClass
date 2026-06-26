using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.DrawLayers
{
	public class DrawLayerHelper : ModSystem
	{
		//TODO TML 1.4.4 or 1.4.5 remove the Pre2026_06 members once version is out
		public static bool UseDrawSittingLegsMethodPre2026_06 => BuildInfo.tMLVersion < new Version(2026, 06, 0, 0);

		public delegate void DrawSittingLegsDelegatePre2026_06(ref PlayerDrawSet drawinfo, Texture2D textureToDraw, Color matchingColor, int shaderIndex = 0, bool glowmask = false);
		public delegate void DrawSittingLegsDelegate(ref PlayerDrawSet drawinfo, Texture2D textureToDraw, Color matchingColor, int shaderIndex = 0, bool glowmask = false, EquipType? equipType = null);

		public static DrawSittingLegsDelegatePre2026_06 DrawSittingLegsMethodPre2026_06 { private set; get; } //Method is too big to bother copying, so use reflection instead. Monitor tml developments
		public static DrawSittingLegsDelegate DrawSittingLegsMethod { private set; get; }

		public override void Load()
		{
			var playerDrawLayersType = typeof(PlayerDrawLayers);
			var drawSittingLegsMethodInfo = playerDrawLayersType.GetMethod("DrawSittingLegs", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
			if (UseDrawSittingLegsMethodPre2026_06)
			{
				DrawSittingLegsMethodPre2026_06 = (DrawSittingLegsDelegatePre2026_06)Delegate.CreateDelegate(typeof(DrawSittingLegsDelegatePre2026_06), drawSittingLegsMethodInfo);
			}
			else
			{
				DrawSittingLegsMethod = (DrawSittingLegsDelegate)Delegate.CreateDelegate(typeof(DrawSittingLegsDelegate), drawSittingLegsMethodInfo);
			}
		}

		public override void Unload()
		{
			DrawSittingLegsMethodPre2026_06 = null;
			DrawSittingLegsMethod = null;
		}

		//Copied from vanilla
		public static bool ShouldOverrideLegs_CheckShoes(ref PlayerDrawSet drawInfo)
		{
			return drawInfo.drawPlayer.shoe > 0 && ArmorIDs.Shoe.Sets.OverridesLegs[drawInfo.drawPlayer.shoe];
		}
	}
}
