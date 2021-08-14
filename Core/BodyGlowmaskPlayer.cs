using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ClickerClass.Core
{
	public class BodyGlowmaskPlayer : ModPlayer
	{
		private static Dictionary<int, Color> BodyColor { get; set; }

		/// <summary>
		/// Add glowmask color associated with the body equip slot here, usually in <see cref="ModType.SetStaticDefaults"/>.
		/// <para>Don't forget the !Main.dedServ check!</para>
		/// </summary>
		/// <param name="bodySlot">Body equip slot</param>
		/// <param name="color">Color</param>
		public static void RegisterData(int bodySlot, Color color)
		{
			if (!BodyColor.ContainsKey(bodySlot))
			{
				BodyColor.Add(bodySlot, color);
			}
		}

		public override void Load()
		{
			BodyColor = new Dictionary<int, Color>();
		}

		public override void Unload()
		{
			BodyColor = null;
		}

		public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
		{
			if (!BodyColor.TryGetValue(Player.body, out Color color))
			{
				return;
			}

			drawInfo.bodyGlowColor = color;
			drawInfo.armGlowColor = color;
		}
	}
}
