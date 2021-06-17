using ClickerClass.Core.Netcode;
using ClickerClass.Effects;
using ClickerClass.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace ClickerClass
{
	public class ClickerClass : Mod
	{
		public static ModHotKey AutoClickKey;
		internal static ClickerClass mod;

		/// <summary>
		/// Populated by the buffs themselves, includes all buffs that bosses should be immune to (so no more manual npc.buffImmune)
		/// </summary>
		internal static HashSet<int> BossBuffImmunity;

		/// <summary>
		/// To prevent certain methods being called when they shouldn't
		/// </summary>
		internal static bool finalizedRegisterCompat = false;

		public override void Load()
		{
			finalizedRegisterCompat = false;
			mod = this;
			BossBuffImmunity = new HashSet<int>();
			AutoClickKey = RegisterHotKey("Clicker Accessory", "G"); //Can't localize this
			ClickerSystem.Load();
			ClickEffect.LoadMiscEffects();
			NetHandler.Load();

			if (!Main.dedServ)
			{
				LoadClient();
			}
		}

		public override void Unload()
		{
			finalizedRegisterCompat = false;
			ShaderManager.Unload();
			ClickerSystem.Unload();
			ClickEffect.Unload();
			NetHandler.Unload();
			ClickerInterfaceResources.Unload();
			AutoClickKey = null;
			BossBuffImmunity = null;
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

		public override void PostSetupContent()
		{
			//Only clicker weapons
			RecipeBrowser_AddToCategory("Clickers", "Weapons", "UI/RecipeBrowser_Clickers", (Item item) =>
			{
				return ClickerSystem.IsClickerWeapon(item);
			});

			//Every other clicker item
			RecipeBrowser_AddToCategory("Clicker Items", "Other", "UI/RecipeBrowser_ClickerItems", (Item item) =>
			{
				return ClickerSystem.IsClickerItem(item) && !ClickerSystem.IsClickerWeapon(item);
			});
		}

		public override void PostAddRecipes()
		{
			finalizedRegisterCompat = true;
			ClickerSystem.FinalizeLocalization();
		}

		public override void AddRecipeGroups()
		{
			ClickerRecipes.AddRecipeGroups();
		}

		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			NetHandler.HandlePackets(reader, whoAmI);
		}

		/// <summary>
		/// Attempts to add a subcategory to Recipe Browser
		/// </summary>
		/// <param name="name">The displayed subcategory name</param>
		/// <param name="category">The parent category</param>
		/// <param name="texture">The 24x24 path to texture within the mod</param>
		/// <param name="predicate">The condition at which an item is listed in this subcategory</param>
		private void RecipeBrowser_AddToCategory(string name, string category, string texture, Predicate<Item> predicate)
		{
			Mod recipeBrowser = ModLoader.GetMod("RecipeBrowser");
			if (recipeBrowser != null && !Main.dedServ)
			{
				recipeBrowser.Call(new object[5]
				{
					"AddItemCategory",
					name,
					category,
					this.GetTexture(texture), // 24x24 icon
					predicate
				});
			}
		}
	}
}
