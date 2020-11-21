using ClickerClass.Effects;
using ClickerClass.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace ClickerClass
{
	public class ClickerClass : Mod
	{
		public static ModHotKey AutoClickKey;
		internal static ClickerClass mod;

		public override void Load()
		{
			mod = this;
			AutoClickKey = RegisterHotKey("Clicker Accessory", "G");
			ClickerSystem.Load();

			if (!Main.dedServ)
			{
				LoadClient();
			}
		}

		private void LoadClient()
		{
			ShaderManager.Load();
			ClickerInterfaceResources.Load();
		}

		private GameTime _lastUpdateUIGameTime;

		public override void UpdateUI(GameTime gameTime)
		{
			_lastUpdateUIGameTime = gameTime;
			ClickerInterfaceResources.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			ClickerInterfaceResources.AddDrawLayers(layers);

			Player player = Main.LocalPlayer;
			for (int i = 0; i < layers.Count; i++)
			{
				//Remove Mouse Cursor
				if (ClickerCursor.CanDrawCursor(player.HeldItem))
				{
					if (Main.cursorOverride == -1 && ClickerConfigClient.Instance.ShowCustomCursors)
					{
						if (layers[i].Name.Equals("Vanilla: Cursor"))
						{
							layers.RemoveAt(i);
							break;
						}
					}
				}
			}
		}

		public override void Unload()
		{
			ShaderManager.Unload();
			ClickerSystem.Unload();
			ClickerInterfaceResources.Unload();
			AutoClickKey = null;
			mod = null;
		}

		public override void AddRecipes()
		{
			ClickerRecipes.AddRecipes();
		}

		public override void AddRecipeGroups()
		{
			ClickerRecipes.AddRecipeGroups();
		}
	}
}
