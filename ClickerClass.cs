using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using ClickerClass.UI;
using ClickerClass.Items;
using ClickerClass.Effects;

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
			ClickerInterfaceResources.Unload();
			AutoClickKey = null;
			mod = null;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(this);
			recipe.AddIngredient(null, "ClickerEmblem", 1);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(ItemID.AvengerEmblem, 1);
			recipe.AddRecipe();
		}
	}
}
