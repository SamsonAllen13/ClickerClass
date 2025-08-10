using ClickerClass.Core.Netcode;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Consumables;
using ClickerClass.Items.Placeable;
using ClickerClass.Items.Weapons.Clickers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass
{
	public class ClickerClass : Mod
	{
		public static ModKeybind AutoClickKey;
		public static ModKeybind AimAssistKey;
		internal static ClickerClass mod;

		internal static int AutoreuseWig_Head_Slot = -1;
		
		//TODO - Clicker Catalogue
		/// <summary>
		/// Keeps track of total number of clickers for the purposes of the 'Clicker Catalogue'
		/// </summary>
		public List<int> totalClickers = new List<int>();
		//public List<Item> totalClickers = new List<Item>();
		
		/// <summary>
		/// Populated by the buffs themselves, includes all buffs that bosses should be immune to (so no more manual npc.buffImmune)
		/// </summary>
		internal static HashSet<int> BossBuffImmunity;

		public override void Load()
		{
			mod = this;
			BossBuffImmunity = new HashSet<int>();
			AutoClickKey = KeybindLoader.RegisterKeybind(this, "ClickerAccessory", "G");
			AimAssistKey = KeybindLoader.RegisterKeybind(this, "ClickerAimAssist", "Mouse3");

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
			AutoreuseWig_Head_Slot = EquipLoader.AddEquipTexture(this, "ClickerClass/DrawLayers/AutoreuseWig_Head", EquipType.Head, name: "AutoreuseWig_Head");
		}

		public override object Call(params object[] args)
		{
			return ClickerModCalls.Call(args);
		}

		public override void PostSetupContent()
		{
			DoWikithisSupport();
			DoColoredDamageTypesSupport();
			DoThoriumModSupport();
			DoRecipeBrowserSupport();
			DoNewBeginningsSupport();

			DoMunchiesSupport();
		}

		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			NetHandler.HandlePackets(reader, whoAmI);
		}

		private static void DoWikithisSupport()
		{
			if (!Main.dedServ && ModLoader.TryGetMod("Wikithis", out Mod wikithis))
			{
				wikithis.Call("AddModURL", mod, "https://terrariamods.wiki.gg/wiki/Clicker_Class/{}");
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

		private static readonly int TerrariumArmorAddClassFocus_ClickAmountDecrease = 2;

		private static void TerrariumArmorAddClassFocus_Effect(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().clickerBonus += TerrariumArmorAddClassFocus_ClickAmountDecrease;
		}

		/// <summary>
		/// see https://thoriummod.wiki.gg/wiki/Mod_Calls
		/// </summary>
		private static void DoThoriumModSupport()
		{
			if (!ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
			{
				return;
			}

			thoriumMod.Call("AddMartianItemID", ModContent.ItemType<HighTechClicker>());

			if (thoriumMod.Version >= new Version(1, 7, 2, 0))
			{
				var damageClass = ModContent.GetInstance<ClickerDamage>();
				string customPrefix = "Focus";
				thoriumMod.Call("TerrariumArmorAddClassFocus",
					damageClass,
					(Action<Player>)TerrariumArmorAddClassFocus_Effect,
					new Color(130, 143, 242),
					damageClass.GetLocalization($"{customPrefix}.Name"),
					damageClass.GetLocalization($"{customPrefix}.Description").WithFormatArgs(TerrariumArmorAddClassFocus_ClickAmountDecrease)
					);
			}
		}

		private static void DoRecipeBrowserSupport()
		{
			if (!ModLoader.TryGetMod("RecipeBrowser", out Mod recipeBrowser))
			{
				return;
			}

			//Only clicker weapons
			RecipeBrowser_AddToCategory(recipeBrowser, "Clickers", "Weapons", "UI/RecipeBrowser_Clickers", (Item item) =>
			{
				return ClickerSystem.IsClickerWeapon(item);
			});

			//Every other clicker item
			RecipeBrowser_AddToCategory(recipeBrowser, "Clicker Items", "Other", "UI/RecipeBrowser_ClickerItems", (Item item) =>
			{
				return ClickerSystem.IsClickerItem(item) && !ClickerSystem.IsClickerWeapon(item);
			});
		}

		/// <summary>
		/// Attempts to add a subcategory to Recipe Browser
		/// </summary>
		/// <param name="name">The displayed subcategory name</param>
		/// <param name="category">The parent category</param>
		/// <param name="texture">The 24x24 path to texture within the mod</param>
		/// <param name="predicate">The condition at which an item is listed in this subcategory</param>
		private static void RecipeBrowser_AddToCategory(Mod recipeBrowser, string name, string category, string texture, Predicate<Item> predicate)
		{
			if (!Main.dedServ)
			{
				recipeBrowser.Call(new object[5]
				{
					"AddItemCategory",
					name,
					category,
					recipeBrowser.Version >= new Version(0, 10, 5) ?
					mod.Assets.Request<Texture2D>(texture) :
					mod.Assets.Request<Texture2D>(texture, AssetRequestMode.ImmediateLoad).Value, // 24x24 icon
					predicate
				});
			}
		}

		/// <summary>
		/// see https://github.com/GabeHasWon/NewBeginnings/wiki/Adding-Custom-Origins
		/// </summary>
		private static void DoNewBeginningsSupport()
		{
			if (ModLoader.TryGetMod("NewBeginnings", out Mod newBeginnings))
			{
				var icon = "UI/NewBeginnings_HackerIcon";
				newBeginnings.Call(
					"AddOrigin",
					mod.Assets.Request<Texture2D>(icon),
					"Hacker",
					"Mods.ClickerClass.NewBeginningsSupport.Hacker",
					new (int, int)[] { (ModContent.ItemType<DesktopComputer>(), 1) },
					ItemID.HiTekSunglasses, ItemID.None, ItemID.None,
					new int[] { ModContent.ItemType<Soda>() },
					100, 20,
					-1, ModContent.ItemType<FaultyClicker>(), -1, -1,
					11, 1
				);
			}
		}

		private static void DoMunchiesSupport()
		{
			if (!Main.dedServ && ModLoader.TryGetMod("Munchies", out Mod munchies))
			{
				munchies.Call("AddSingleConsumable", mod, new Version(1, 3).ToString(), ModContent.GetInstance<HeavenlyChip>(), "player", () => Main.LocalPlayer.GetModPlayer<ClickerPlayer>().consumedHeavenlyChip);
			}
		}
	}

	/// <summary>
	/// Handles mod call support for Mod of Redemption.
	/// See <a href="https://modofredemption.wiki.gg/wiki/Mod_Calls">MoR wiki</a> for further information.
	/// </summary>
	public static class MoRSupportHelper
	{
		public const short Arcane = 1;
		public const short Fire = 2;
		public const short Water = 3;
		public const short Ice = 4;
		public const short Earth = 5;
		public const short Wind = 6;
		public const short Thunder = 7;
		public const short Holy = 8;
		public const short Shadow = 9;
		public const short Nature = 10;
		public const short Poison = 11;
		public const short Blood = 12;
		public const short Psychic = 13;
		public const short Celestial = 14;
		public const short Explosive = 15;

		/// <summary>
		/// Registers an item under a given <a href="https://modofredemption.wiki.gg/wiki/Elemental_damage">MoR element</a>, applying damage multipliers based on the element and enemy type, and other unique effects based on the element.
		/// </summary>
		/// <param name="item">The Item to apply the element to.</param>
		/// <param name="elementID">The ID of the element to apply to the item. Use <see cref="MoRSupportHelper">MoRSupportHelper</see> consts (ex. <see cref="MoRSupportHelper.Fire">MoRSupportHelper.Fire</see>).</param>
		/// <param name="projsInheritItemElements">Whether the element should also be applied to any projectiles spawned by the item. Defaults to false.</param>
		public static void RegisterElement(this Item item, int elementID, bool projsInheritItemElements = false)
		{
			if (ModLoader.TryGetMod("Redemption", out Mod redemptionMod))
			{
				redemptionMod.Call("addElementItem", elementID, item.type, projsInheritItemElements);
			}
		}

		/// <summary>
		/// Registers a projectile under a given <a href="https://modofredemption.wiki.gg/wiki/Elemental_damage">MoR element</a>, applying damage multipliers based on the element and enemy type, and other unique effects based on the element.
		/// </summary>
		/// <param name="projectile">The Projectile to apply the element to.</param>
		/// <param name="elementID">The ID of the element to apply to the item. Use <see cref="MoRSupportHelper">MoRSupportHelper</see> consts (ex. <see cref="MoRSupportHelper.Fire">MoRSupportHelper.Fire</see>).</param>
		/// <param name="projsInheritProjElements">Whether the element should also be applied to any projectiles spawned by the projectile. Defaults to false.</param>
		public static void RegisterElement(this Projectile projectile, int elementID, bool projsInheritProjElements = false)
		{
			if (ModLoader.TryGetMod("Redemption", out Mod redemptionMod))
			{
				redemptionMod.Call("addElementProj", elementID, projectile.type, projsInheritProjElements);
			}
		}

		/// <summary>
		/// Checks if a given attack will trigger MoR's <a href="https://modofredemption.wiki.gg/wiki/Elemental_damage#Decapitation">decapitation</a> mechanic, instantly killing certain enemies and dropping their heads if applicable.
		/// To be called on an onHitNPC() function.
		/// </summary>
		/// <param name="target">The NPC to try the decapitation on. This is normally the target parameter from an onHitNPC() function.</param>
		/// <param name="damageDone">The damage dealt by the attack to try the decapitation on. This is normally the damageDone parameter from an onHitNPC() function.</param>
		/// <param name="isCrit">Whether the attack to try the decapitation on was a critical hit. This is normally the hit.Crit parameter from an onHitNPC() function.</param>
		/// <param name="chance">The chance the attack successfully decapitates or not. Defaults to 200 (1/200 chance).</param>
		public static void TryDecapitation(NPC target, int damageDone, bool isCrit, int chance = 200)
		{
			if (ModLoader.TryGetMod("Redemption", out Mod redemptionMod))
			{
				redemptionMod.Call("decapitation", target, damageDone, isCrit, chance);
			}
		}

		public static bool InWasteland(Player player)
		{
			if (ModLoader.TryGetMod("Redemption", out Mod redemptionMod))
			{
				if (redemptionMod.TryFind<ModBiome>("WastelandPurityBiome", out ModBiome wastelandBiome))
				{
					return player.InModBiome(wastelandBiome);
				}
			}
			return false;
		}
	}

	public static class ThoriumModSupportHelper
	{
		public static bool InAquaticDepths(Player player)
		{
			if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
			{
				if (thoriumMod.TryFind<ModBiome>("DepthsBiome", out ModBiome depthsBiome))
				{
					return player.InModBiome(depthsBiome);
				}
			}
			return false;
		}
	}

	public static class CalamityModSupportHelper
	{
		public static bool InBiome(Player player, String biomeName)
		{
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				return (bool)calamityMod.Call("GetInZone", player, biomeName);
			}
			return false;
		}
	}
}
