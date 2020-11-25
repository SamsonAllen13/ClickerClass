using ClickerClass.Effects;
using ClickerClass.Items;
using ClickerClass.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace ClickerClass
{
	public class ClickerClass : Mod
	{
		public static ModHotKey AutoClickKey;
		internal static ClickerClass mod;

		/// <summary>
		/// To prevent certain methods being called when they shouldn't
		/// </summary>
		internal static bool finalizedRegisterCompat = false;

		public override void Load()
		{
			finalizedRegisterCompat = false;
			mod = this;
			AutoClickKey = RegisterHotKey("Clicker Accessory", "G");
			ClickerSystem.Load();

			if (!Main.dedServ)
			{
				LoadClient();
			}

			#region Translations

			ModTranslation text = CreateTranslation("miceSetBonus");
			text.SetDefault("Right clicking a position within your clicker radius will teleport you to it");
			text.AddTranslation(GameCulture.Russian, "Нажмите ПКМ, чтобы телепортироваться в пределах радиуса курсора");
			AddTranslation(text);
			
			text = CreateTranslation("motherboardSetBonus");
			text.SetDefault("Right click to place a radius extending sensor");
			text.AddTranslation(GameCulture.Russian, "Нажмите ПКМ, чтобы поставить датчик расширения радиуса курсора");
			AddTranslation(text);
			
			text = CreateTranslation("overclockSetBonus");
			text.SetDefault("Every 100 clicks briefly grants you 'Overclock', making every click trigger its effect");
			text.AddTranslation(GameCulture.Russian, "Каждые 100 кликов на короткий промежуток времени дарует бафф «Разгон», заставляя каждый клик активировать свой эффект");
			AddTranslation(text);
			
			text = CreateTranslation("precursorSetBonus");
			text.SetDefault("Clicking causes an additional delayed fiery click at 25% the damage");
			text.AddTranslation(GameCulture.Russian, "Кликанье вызывает дополнительный запоздалый огненный клик наносящий 25% урона текущего курсора");
			AddTranslation(text);

			#endregion
		}

		public override void Unload()
		{
			finalizedRegisterCompat = false;
			ShaderManager.Unload();
			ClickerSystem.Unload();
			ClickerInterfaceResources.Unload();
			AutoClickKey = null;
			mod = null;
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
							layers.RemoveAt(i);
							break;
						}
					}
				}
			}
		}

		public override object Call(params object[] args)
		{
			return ClickerModCalls.Call(args);
		}

		public override void AddRecipes()
		{
			ClickerRecipes.AddRecipes();
		}

		public override void PostAddRecipes()
		{
			finalizedRegisterCompat = true;
		}

		public override void AddRecipeGroups()
		{
			ClickerRecipes.AddRecipeGroups();
		}
	}
}
