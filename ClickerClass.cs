using ClickerClass.Core.Netcode;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace ClickerClass
{
	public class ClickerClass : Mod
	{
		public static ModKeybind AutoClickKey;
		public static ModKeybind AimAssistKey;
		internal static ClickerClass mod;

		/// <summary>
		/// Populated by the buffs themselves, includes all buffs that bosses should be immune to (so no more manual npc.buffImmune)
		/// </summary>
		internal static HashSet<int> BossBuffImmunity;

		public override void Load()
		{
			mod = this;
			BossBuffImmunity = new HashSet<int>();
			AutoClickKey = KeybindLoader.RegisterKeybind(this, "ClickerAccessory", "G"); //Can't localize this
			AimAssistKey = KeybindLoader.RegisterKeybind(this, "ClickerAimAssist", "Mouse3"); //Can't localize this

			NetHandler.Load();

			if (!Main.dedServ)
			{
				LoadClient();
			}
		}

		public override void Unload()
		{
			ClickEffect.Unload();
			NetHandler.Unload();
			AutoClickKey = null;
			AimAssistKey = null;
			BossBuffImmunity = null;
			mod = null;
		}

		private void LoadClient()
		{

		}

		public override object Call(params object[] args)
		{
			return ClickerModCalls.Call(args);
		}

		public override void PostSetupContent()
		{
			DoWikithisSupport();
			DoColoredDamageTypesSupport();

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

		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			NetHandler.HandlePackets(reader, whoAmI);
		}

		private static void DoWikithisSupport()
		{
			if (!Main.dedServ && ModLoader.TryGetMod("Wikithis", out Mod wikithis))
			{
				wikithis.Call("AddModURL", mod, "terrariamods.wiki.gg$Clicker_Class");
			}
		}

		private static void DoColoredDamageTypesSupport()
		{
			if (!Main.dedServ && ModLoader.TryGetMod("ColoredDamageTypes", out Mod coloreddamagetypes))
			{
				var tooltipColor = (130, 143, 242);
				var damageColor = (172, 189, 246);
				var critColor = (88, 92, 222);
				coloreddamagetypes.Call("AddDamageType", ModContent.GetInstance<ClickerDamage>(), tooltipColor, damageColor, critColor);
			}
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
			if (!Main.dedServ && ModLoader.TryGetMod("RecipeBrowser", out Mod recipeBrowser))
			{
				recipeBrowser.Call(new object[5]
				{
					"AddItemCategory",
					name,
					category,
					this.Assets.Request<Texture2D>(texture, AssetRequestMode.ImmediateLoad).Value, // 24x24 icon
					predicate
				});
			}
		}
	}
}
