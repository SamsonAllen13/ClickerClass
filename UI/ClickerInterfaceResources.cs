using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.UI;

namespace ClickerClass.UI
{
	internal class ClickerInterfaceResources
	{
		private static List<InterfaceResource> Resources;

		internal static void Load()
		{
			Resources = new List<InterfaceResource>
			{
				new ClickerCursor(),
				new HotKeychainGauge(),
				new PaperclipsGauge()
			};
		}

		internal static void Unload()
		{
			Resources = null;
		}

		internal static void Update(GameTime gameTime)
		{
			if (Resources != null)
			{
				foreach (var resource in Resources)
				{
					resource.Update(gameTime);
				}
			}
		}

		internal static void AddDrawLayers(List<GameInterfaceLayer> layers)
		{
			if (Resources != null)
			{
				foreach (var resource in Resources)
				{
					int layer = resource.GetInsertIndex(layers);
					if (layer != -1)
					{
						layers.Insert(layer, resource);
					}
				}
			}
		}
	}
}
