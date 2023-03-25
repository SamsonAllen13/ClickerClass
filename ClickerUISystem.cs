using System.Reflection;
using System.Collections.Generic;
using ClickerClass.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using System.Linq;
using System;

namespace ClickerClass
{
	[Autoload(true, Side = ModSide.Client)]
	public class ClickerUISystem : ModSystem
	{
		private GameTime _lastUpdateUIGameTime;
		private bool reflectionFailedOnceDoNotRetry = false;

		public override void UpdateUI(GameTime gameTime)
		{
			_lastUpdateUIGameTime = gameTime;
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			ClickerInterfaceResources.AddInterfaceLayers(layers);

			//Remove Mouse Cursor
			if (Main.cursorOverride == -1 && ClickerConfigClient.Instance.ShowCustomCursors)
			{
				Player player = Main.LocalPlayer;
				if (ClickerCursor.CanDrawCursor(player.HeldItem))
				{
					for (int i = 0; i < layers.Count; i++)
					{
						if (layers[i].Name.Equals("Vanilla: Cursor"))
						{
							//This only removes the default cursor, see DetourSecondCursor for the second one
							layers.RemoveAt(i);
							break;
						}
					}
				}
			}
		}

		public override void OnModLoad()
		{
			On_Main.DrawInterface_36_Cursor += DetourSecondCursor;
		}

		public override void OnModUnload()
		{
			reflectionFailedOnceDoNotRetry = false;
		}

		private void DetourSecondCursor(On_Main.orig_DrawInterface_36_Cursor orig)
		{
			//This is used to detour the second cursor draw which happens on NPC mouseover in DrawInterface_41 after Main.instance._mouseTextCache.valid is true
			if (!reflectionFailedOnceDoNotRetry && ClickerCursor.detourSecondCursorDraw)
			{
				ClickerCursor.detourSecondCursorDraw = false;

				try
				{
					/*if (instance._mouseTextCache.isValid) {
							instance.MouseTextInner(instance._mouseTextCache);
							DrawInterface_36_Cursor(); //Detour only this one
							instance._mouseTextCache.isValid = false;
							instance._mouseTextCache.noOverride = false;
						}
					 */

					Type mainType = typeof(Main);
					FieldInfo mouseTextCacheField = mainType.GetField("_mouseTextCache", BindingFlags.NonPublic | BindingFlags.Instance);
					var mouseTextCache = mouseTextCacheField.GetValue(Main.instance);

					var nestedTypes = mainType.GetNestedTypes(BindingFlags.Static | BindingFlags.NonPublic);
					var mouseTextCacheType = nestedTypes.FirstOrDefault(t => t.Name == "MouseTextCache");
					FieldInfo isValidField = mouseTextCacheType.GetField("isValid", BindingFlags.Public | BindingFlags.Instance);
					object isValid = isValidField.GetValue(mouseTextCache);

					if (isValid is bool valid && valid)
					{
						return;
					}
				}
				catch
				{
					reflectionFailedOnceDoNotRetry = true;
				}
			}

			orig();
		}
	}
}
