using ClickerClass.Items;
using ClickerClass.Items.Misc;
using ClickerClass.Projectiles;
using ClickerClass.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass
{
	/// <summary>
	/// Manages registering clicker class related content and provides basic methods to check for content being clicker class related
	/// </summary>
	public class ClickerSystem : ModSystem
	{
		//Clientside only hence why player instance to play the sound at is not necessary

		/// <summary>
		/// To prevent certain methods being called when they shouldn't
		/// </summary>
		internal static bool FinalizedRegisterCompat { get; private set; }

		internal static LocalizedText UnknownText { get; private set; }

		internal static LocalizedText DefaultClickerWeaponTooltipText { get; private set; }

		private static HashSet<int> ClickerItems { get; set; }

		private static Dictionary<int, string> ClickerWeaponBorderTexture { get; set; }

		private static HashSet<int> ClickerWeapons { get; set; }
		private static Dictionary<string, HashSet<int>> ClickerWeaponsByMod { get; set; }
		public static List<string> SortedModsByClickerWeaponCount { get; private set; }
		/// <summary>
		/// If not in this dict, assumed to always be obtainable
		/// </summary>
		private static Dictionary<int, Func<bool>> ObtainmentConditionsByClickerWeapon { get; set; }
		public static Dictionary<string, List<Func<bool>>> ObtainmentConditionsByMod { get; private set; }

		/// <summary>
		/// Keeps track of sorted view for 'Clicker Catalogue'
		/// </summary>
		public static List<int> SortedClickerWeapons { get; private set; }

		private static Dictionary<int, Action<int>> SFXButtons { get; set; }

		private static HashSet<int> ClickerWeaponProjectiles { get; set; }

		private static HashSet<int> ClickerProjectiles { get; set; }

		/// <summary>
		/// Contains hint tooltips for each item, if they exist
		/// </summary>
		internal static Dictionary<int, LocalizedText> HintTooltipTexts { get; private set; }

		/// <summary>
		/// A dictionary containing registered (!) ClickEffects. When "creating" new ones to assign to something, it clones it from this
		/// </summary>
		private static Dictionary<string, ClickEffect> ClickEffectsByName { get; set; }

		/// <summary>
		/// A dictionary containing <see cref="ClickEffect.DisplayName"/>.
		/// </summary>
		internal static Dictionary<string, LocalizedText> DisplayNamesByName { get; private set; }

		/// <summary>
		/// A dictionary containing <see cref="ClickEffect.Description"/>.
		/// </summary>
		internal static Dictionary<string, LocalizedText> DescriptionsByName { get; private set; }

		public override void OnModLoad()
		{
			FinalizedRegisterCompat = false;
			UnknownText = Language.GetOrRegister(Mod.GetLocalizationKey("Common.Unknown"));
			DefaultClickerWeaponTooltipText = Language.GetOrRegister(Mod.GetLocalizationKey("Common.Tooltips.Clicker"));
			ClickerItems = new HashSet<int>();
			ClickerWeaponBorderTexture = new Dictionary<int, string>();
			ClickerWeapons = new HashSet<int>();
			ClickerWeaponsByMod = new Dictionary<string, HashSet<int>>();
			SortedModsByClickerWeaponCount = new List<string>();
			ObtainmentConditionsByClickerWeapon = new Dictionary<int, Func<bool>>();
			ObtainmentConditionsByMod = new Dictionary<string, List<Func<bool>>>();
			SortedClickerWeapons = new List<int>();
			SFXButtons = new Dictionary<int, Action<int>>();
			ClickerProjectiles = new HashSet<int>();
			ClickerWeaponProjectiles = new HashSet<int>();
			HintTooltipTexts = new Dictionary<int, LocalizedText>();
			ClickEffectsByName = new Dictionary<string, ClickEffect>();
			DisplayNamesByName = new Dictionary<string, LocalizedText>();
			DescriptionsByName = new Dictionary<string, LocalizedText>();
		}

		public override void OnModUnload()
		{
			FinalizedRegisterCompat = false;
			ClickerItems = null;
			ClickerWeaponBorderTexture?.Clear();
			ClickerWeaponBorderTexture = null;
			ClickerWeapons = null;
			ClickerWeaponsByMod = null;
			SortedModsByClickerWeaponCount = null;
			ObtainmentConditionsByClickerWeapon = null;
			ObtainmentConditionsByMod = null;
			SortedClickerWeapons = null;
			SFXButtons = null;
			ClickerProjectiles = null;
			ClickerWeaponProjectiles = null;
			HintTooltipTexts?.Clear();
			HintTooltipTexts = null;
			ClickEffectsByName?.Clear();
			ClickEffectsByName = null;
			DisplayNamesByName?.Clear();
			DisplayNamesByName = null;
			DescriptionsByName?.Clear();
			DescriptionsByName = null;
		}

		public override void SetStaticDefaults()
		{
			ClickEffect.LoadMiscEffects();
		}

		public override void PostAddRecipes()
		{
			SortedModsByClickerWeaponCount = ClickerWeaponsByMod.Keys
				.Where(x => ClickerWeaponsByMod[x].Count > 0)
				.OrderBy(x => ClickerWeaponsByMod[x].Count)
				.ToList();

			FinalizedRegisterCompat = true;
		}

		//A bug with this is that when this actually triggers (new lang entry) in the same game session it will not properly elaborate into the value, needs a lang file reload.
		//It's fine on subsequent game launches though and will not be a problem for players since this only manifests during development
		internal static string GetUnknownTextInterpolation() => $"{{${UnknownText.Key}}}";

		/// <summary>
		/// Adds an obtainment hint tooltip to the given item type. If null or <see cref="LocalizedText.Empty"/>, will not be added
		/// </summary>
		/// <param name="itemType">The item type</param>
		/// <param name="hintTooltip">The hint's <see cref="LocalizedText"/></param>
		/// <returns><see langword="true"/> if successfully added</returns>
		public static bool TryAddHintTooltipText(int itemType, LocalizedText hintTooltip)
		{
			if (hintTooltip == null || hintTooltip == LocalizedText.Empty)
			{
				return false;
			}

			HintTooltipTexts[itemType] = hintTooltip;
			return true;
		}

		/// <summary>
		/// Checks if an item has already defined its hint tooltip, and assigns it
		/// </summary>
		/// <param name="itemType">The item type</param>
		/// <param name="hintTooltip">The hint's <see cref="LocalizedText"/></param>
		/// <returns><see langword="true"/> if a hint tooltip exists</returns>
		public static bool TryGetHintTooltipText(int itemType, out LocalizedText hintTooltip)
		{
			hintTooltip = null;
			if (HintTooltipTexts.TryGetValue(itemType, out LocalizedText other))
			{
				hintTooltip = other;
				return hintTooltip != LocalizedText.Empty;
			}
			return false;
		}

		public static string UniqueEffectName(Mod mod, string internalName) => $"{mod.Name}:{internalName}";

		/// <summary>
		/// Returns the effect dictionary
		/// </summary>
		/// <returns>IReadOnlyDictionary[string, ClickEffect]</returns>
		public static IReadOnlyDictionary<string, ClickEffect> GetAllEffects()
		{
			return ClickEffectsByName;
		}

		/// <summary>
		/// Returns all existing effects' internal names
		/// </summary>
		/// <returns>List[string]</returns>
		public static List<string> GetAllEffectNames()
		{
			//Mod compat version of GetAllEffects() since ClickEffect is an unknown type
			return GetAllEffects().Keys.ToList();
		}

		/// <summary>
		/// Mod Compat way of accessing an effect's stats. <see cref="null"/> if not found.
		/// "Mod": The mod the effect belongs to (Mod).
		/// | "InternalName": The internal name (string).
		/// | "UniqueName": The unique name (string) (should match the input string).
		/// | "DisplayName": The displayed name (LocalizedText).
		/// | "Description": The description (LocalizedText).
		/// | "Amount": The amount of clicks to trigger the effect (int).
		/// | "ColorFunc": The color (Color) if invoked.
		/// | "Action": The method ran when triggered (Action[Player, EntitySource_ItemUse_WithAmmo, Vector2, int, int, float]).
		/// | "PreHardMode": Belongs to something available pre-hardmode (bool).
		/// </summary>
		/// <param name="name">The unique name</param>
		/// <returns>Dictionary[string, object]</returns>
		internal static Dictionary<string, object> GetClickEffectAsDict(string name)
		{
			if (IsClickEffect(name, out ClickEffect effect))
			{
				return effect.ToDictionary();
			}
			return null;
		}

		/// <summary>
		/// Checks if an effect of this name exists
		/// </summary>
		/// <param name="name">The unique name</param>
		/// <returns><see langword="true"/> if valid</returns>
		public static bool IsClickEffect(string name)
		{
			return ClickEffectsByName.TryGetValue(name, out _);
		}

		/// <summary>
		/// Checks if an effect of this name exists, and assigns it
		/// </summary>
		/// <param name="name">The unique name</param>
		/// <param name="effect">The <see cref="ClickEffect"/> associated with this name</param>
		/// <returns><see langword="true"/> if valid</returns>
		public static bool IsClickEffect(string name, out ClickEffect effect)
		{
			effect = null;
			if (ClickEffectsByName.TryGetValue(name, out ClickEffect other))
			{
				effect = (ClickEffect)other.Clone();
				return true;
			}
			return false;
		}

		/// <summary>
		/// Checks if an effect has already defined its display name, and assigns it
		/// </summary>
		/// <param name="name">The unique name</param>
		/// <param name="displayName">The display name's <see cref="LocalizedText"/></param>
		/// <returns><see langword="true"/> if already defined</returns>
		public static bool TryGetClickEffectName(string name, out LocalizedText displayName)
		{
			displayName = null;
			if (DisplayNamesByName.TryGetValue(name, out LocalizedText other))
			{
				displayName = other;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Checks if an effect has already defined its description, and assigns it
		/// </summary>
		/// <param name="name">The unique name</param>
		/// <param name="description">The description's <see cref="LocalizedText"/></param>
		/// <returns><see langword="true"/> if already defined</returns>
		public static bool TryGetClickEffectDescription(string name, out LocalizedText description)
		{
			description = null;
			if (DescriptionsByName.TryGetValue(name, out LocalizedText other))
			{
				description = other;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Call this in <see cref="Mod.PostSetupContent"/> or <see cref="ModType.SetStaticDefaults"/> to register this click effect
		/// </summary>
		/// <param name="mod">The mod this effect belongs to. ONLY USE YOUR OWN MOD INSTANCE FOR THIS!</param>
		/// <param name="internalName">The internal name of the effect. Turns into the unique name combined with the associated mod</param>
		/// <param name="amount">The amount of clicks required to trigger the effect</param>
		/// <param name="colorFunc">The (dynamic) text color representing the effect in the tooltip</param>
		/// <param name="action">The method that runs when the effect is triggered</param>
		/// <param name="preHardMode">If this effect primarily belongs to something available pre-hardmode</param>
		/// <param name="nameArgs">Arguments that need to be bound to the display name</param>
		/// <param name="descriptionArgs">Arguments that need to be bound to the description</param>
		/// <returns>The unique identifier</returns>
		/// <exception cref="InvalidOperationException"/>
		public static string RegisterClickEffect(Mod mod, string internalName, int amount, Func<Color> colorFunc, Action<Player, EntitySource_ItemUse_WithAmmo, Vector2, int, int, float> action, bool preHardMode = false, object[] nameArgs = null, object[] descriptionArgs = null)
		{
			if (FinalizedRegisterCompat)
			{
				throw new InvalidOperationException("Tried to register a click effect at the wrong time, do so in Mod.PostSetupContent or ModItem.SetStaticDefaults");
			}
			if (string.IsNullOrEmpty(internalName))
			{
				throw new InvalidOperationException($"internalName is either null or empty. Give it a proper value");
			}

			string uniqueName = UniqueEffectName(mod, internalName);
			if (!IsClickEffect(uniqueName))
			{
				ClickEffect effect = new ClickEffect(mod, internalName, amount, colorFunc, action, preHardMode, nameArgs, descriptionArgs);

				ClickEffectsByName.Add(uniqueName, effect);
				DisplayNamesByName.Add(uniqueName, effect.DisplayName);
				DescriptionsByName.Add(uniqueName, effect.Description);
				return uniqueName;
			}
			else
			{
				throw new InvalidOperationException($"The effect '{uniqueName}' has already been registered, duplicate detected");
			}
		}

		/// <summary>
		/// Call this in <see cref="Mod.PostSetupContent"/> or <see cref="ModType.SetStaticDefaults"/> to register this click effect
		/// </summary>
		/// <param name="mod">The mod this effect belongs to. ONLY USE YOUR OWN MOD INSTANCE FOR THIS!</param>
		/// <param name="internalName">The internal name of the effect. Turns into the unique name combined with the associated mod</param>
		/// <param name="amount">The amount of clicks required to trigger the effect</param>
		/// <param name="color">The text color representing the effect in the tooltip</param>
		/// <param name="action">The method that runs when the effect is triggered</param>
		/// <remarks>For dynamic colors, use the Func[Color] overload</remarks>
		/// <param name="preHardMode">If this effect primarily belongs to something available pre-hardmode</param>
		/// <param name="nameArgs">Arguments that need to be bound to the display name</param>
		/// <param name="descriptionArgs">Arguments that need to be bound to the description</param>
		/// <returns>The unique identifier</returns>
		/// <exception cref="InvalidOperationException"/>
		public static string RegisterClickEffect(Mod mod, string internalName, int amount, Color color, Action<Player, EntitySource_ItemUse_WithAmmo, Vector2, int, int, float> action, bool preHardMode = false, object[] nameArgs = null, object[] descriptionArgs = null)
		{
			return RegisterClickEffect(mod, internalName, amount, () => color, action, preHardMode, nameArgs, descriptionArgs);
		}

		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> to set important default fields for a clicker weapon. Set fields:
		/// DamageType, useTime, useAnimation, useStyle, holdStyle, noMelee, shoot, shootSpeed.
		/// Only change them afterwards if you know what you are doing!
		/// </summary>
		/// <param name="item">The <see cref="Item"/> to set the defaults for</param>
		public static void SetClickerWeaponDefaults(Item item)
		{
			item.DamageType = ModContent.GetInstance<ClickerDamage>();
			item.useTime = 2;
			item.useAnimation = 2;
			item.useStyle = ItemUseStyleID.Shoot;
			item.holdStyle = 3;
			item.noMelee = true;
			item.shoot = ModContent.ProjectileType<ClickDamage>();
			item.shootSpeed = 1f;
		}

		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> to set important default fields for a "sfx button". Set fields:
		/// maxStack.
		/// Only change them afterwards if you know what you are doing!
		/// </summary>
		/// <param name="item">The <see cref="Item"/> to set the defaults for</param>
		public static void SetSFXButtonDefaults(Item item)
		{
			item.maxStack = SFXButtonBase.StackAmount;
		}

		/// <summary>
		/// Call in <see cref="ModProjectile.SetDefaults"/> to set important default fields for a clicker projectile. Set fields:
		/// DamageType.
		/// Only change them afterwards if you know what you are doing!
		/// </summary>
		/// <param name="projectile">The <see cref="Projectile"/> to set the defaults for</param>
		public static void SetClickerProjectileDefaults(Projectile projectile)
		{
			projectile.DamageType = ModContent.GetInstance<ClickerDamage>();
		}

		/// <summary>
		/// Call this in <see cref="ModType.SetStaticDefaults"/> to register this projectile into the "clicker class" category.
		/// This will apply armor penetration and hit direction defaults
		/// </summary>
		/// <param name="modProj">The <see cref="ModProjectile"/> that is to be registered</param>
		/// <exception cref="InvalidOperationException"/>
		public static void RegisterClickerProjectile(ModProjectile modProj)
		{
			if (FinalizedRegisterCompat)
			{
				throw new InvalidOperationException("Tried to register a clicker projectile at the wrong time, do so in ModProjectile.SetStaticDefaults");
			}
			int type = modProj.Projectile.type;
			if (!ClickerProjectiles.Add(type)) return;

			//Extra registration code here
		}

		/// <summary>
		/// Call this in <see cref="ModType.SetStaticDefaults"/> to register this projectile into the "clicker weapon" category.
		/// <br>This is only for projectiles spawned by clickers directly (Item.shoot). Clicker Class only uses one such projectile for all it's clickers. Only use this if you know what you are doing!</br>
		/// <br>Various effects will only proc "on click" by checking this category instead of "all clicker class projectiles"</br>
		/// </summary>
		/// <param name="modProj">The <see cref="ModProjectile"/> that is to be registered</param>
		/// <exception cref="InvalidOperationException"/>
		public static void RegisterClickerWeaponProjectile(ModProjectile modProj)
		{
			if (FinalizedRegisterCompat)
			{
				throw new InvalidOperationException("Tried to register a clicker weapon projectile at the wrong time, do so in ModProjectile.SetStaticDefaults");
			}
			int type = modProj.Projectile.type;
			if (!ClickerWeaponProjectiles.Add(type)) return;

			//Extra registration code here
		}

		/// <summary>
		/// Call this in <see cref="ModType.SetStaticDefaults"/> to register this item into the "clicker class" category
		/// </summary>
		/// <param name="modItem">The <see cref="ModItem"/> that is to be registered</param>
		/// <exception cref="InvalidOperationException"/>
		public static void RegisterClickerItem(ModItem modItem)
		{
			if (FinalizedRegisterCompat)
			{
				throw new InvalidOperationException("Tried to register a clicker item at the wrong time, do so in ModItem.SetStaticDefaults");
			}
			int type = modItem.Item.type;
			if (!ClickerItems.Add(type)) return;

			//Extra registration code here
		}

		/// <summary>
		/// Call this in <see cref="ModType.SetStaticDefaults"/> to register this weapon into the "clicker class" category as a "clicker".<br/>
		/// Do not call <see cref="RegisterClickerItem"/> with it as this method does this already by itself
		/// </summary>
		/// <param name="modItem">The <see cref="ModItem"/> that is to be registered</param>
		/// <param name="borderTexture">The path to the border texture (optional)</param>
		/// <param name="hintTooltip">A custom obtainment hint tooltip. If left unassigned, will be automatically generated in the localization file for your item. If set to <see cref="LocalizedText.Empty"/>, no hint tooltip will be set.</param>
		/// <param name="obtainmentCondition">A custom obtainment condition. This should be generic and only used for things where an item cannot be obtained legit, like a specific world seed or config toggle. If left unassigned, will be assumed to always obtainable.</param>
		/// <exception cref="InvalidOperationException"/>
		public static void RegisterClickerWeapon(ModItem modItem, string borderTexture = null, LocalizedText hintTooltip = null, Func<bool> obtainmentCondition = null)
		{
			if (FinalizedRegisterCompat)
			{
				throw new InvalidOperationException("Tried to register a clicker weapon at the wrong time, do so in ModItem.SetStaticDefaults");
			}
			RegisterClickerItem(modItem);
			int type = modItem.Item.type;
			if (!ClickerWeapons.Add(type)) return;

			if (!ClickerWeaponsByMod.TryGetValue(modItem.Mod.Name, out HashSet<int> modClickers))
			{
				modClickers = new HashSet<int>();
				ClickerWeaponsByMod[modItem.Mod.Name] = modClickers;
			}
			modClickers.Add(type);

			if (obtainmentCondition != null)
			{
				ObtainmentConditionsByClickerWeapon[type] = obtainmentCondition;

				if (!ObtainmentConditionsByMod.TryGetValue(modItem.Mod.Name, out List<Func<bool>> modConditions))
				{
					modConditions = new List<Func<bool>>();
					ObtainmentConditionsByMod[modItem.Mod.Name] = modConditions;
				}
				modConditions.Add(obtainmentCondition);
			}

			if (borderTexture != null)
			{
				if (ModContent.HasAsset(borderTexture))
				{
					ClickerWeaponBorderTexture.TryAdd(type, borderTexture);
				}
				else
				{
					ClickerClass.mod.Logger.Info($"Border texture for {modItem.Name} not found: {borderTexture}");
				}
			}

			hintTooltip ??= modItem.GetLocalization("Hint", GetUnknownTextInterpolation);
			TryAddHintTooltipText(modItem.Type, hintTooltip);
		}

		/// <summary>
		/// Call this in <see cref="ModType.SetStaticDefaults"/> to register this item into the "sfx button" category.<br/>
		/// It will automatically contribute to the active "sfx buttons" when in the inventory<br/>
		/// Do not call <see cref="RegisterClickerItem"/> with it as this method does this already by itself
		/// </summary>
		/// <param name="modItem">The <see cref="ModItem"/> that is to be registered</param>
		/// <param name="playSoundAction">The method that runs that will play the sound</param>
		/// <exception cref="InvalidOperationException"/>
		public static void RegisterSFXButton(ModItem modItem, Action<int> playSoundAction)
		{
			if (FinalizedRegisterCompat)
			{
				throw new InvalidOperationException("Tried to register an sfx button at the wrong time, do so in ModItem.SetStaticDefaults");
			}
			RegisterClickerItem(modItem);
			int type = modItem.Item.type;
			if (!SFXButtons.TryAdd(type, playSoundAction)) return;

			//Extra registration code here
		}

		/// <summary>
		/// Returns the border texture of the item of this type
		/// </summary>
		/// <param name="type">The item type</param>
		/// <returns>The path to the border texture, null if not found</returns>
		public static string GetPathToBorderTexture(int type)
		{
			if (ClickerWeaponBorderTexture.TryGetValue(type, out string borderTexture))
			{
				return borderTexture;
			}
			return null;
		}

		/// <summary>
		/// Call this to check if a projectile type belongs to the "clicker class" category
		/// </summary>
		/// <param name="type">The projectile type to be checked</param>
		/// <returns><see langword="true"/> if that category</returns>
		public static bool IsClickerProj(int type)
		{
			return ClickerProjectiles.Contains(type);
		}

		/// <summary>
		/// Call this to check if a projectile belongs to the "clicker class" category
		/// </summary>
		/// <param name="proj">The <see cref="Projectile"/> to be checked</param>
		/// <returns><see langword="true"/> if that category</returns>
		public static bool IsClickerProj(Projectile proj)
		{
			return IsClickerProj(proj.type);
		}

		/// <summary>
		/// Call this to check if a projectile type belongs to the "clicker weapon" category.
		/// <br>Various effects will only proc "on click" by checking this category instead of "all clicker class projectiles"</br>
		/// </summary>
		/// <param name="type">The projectile type to be checked</param>
		/// <returns><see langword="true"/> if that category</returns>
		public static bool IsClickerWeaponProj(int type)
		{
			return ClickerWeaponProjectiles.Contains(type);
		}

		/// <summary>
		/// Call this to check if a projectile belongs to the "clicker weapon" category.
		/// <br>Various effects will only proc "on click" by checking this category instead of "all clicker class projectiles"</br>
		/// </summary>
		/// <param name="proj">The <see cref="Projectile"/> to be checked</param>
		/// <returns><see langword="true"/> if that category</returns>
		public static bool IsClickerWeaponProj(Projectile proj)
		{
			return IsClickerWeaponProj(proj.type);
		}

		/// <summary>
		/// Call this to check if an item type belongs to the "clicker class" category
		/// </summary>
		/// <param name="type">The item type to be checked</param>
		/// <returns><see langword="true"/> if that category</returns>
		public static bool IsClickerItem(int type)
		{
			return ClickerItems.Contains(type);
		}

		/// <summary>
		/// Call this to check if an item belongs to the "clicker class" category
		/// </summary>
		/// <param name="item">The <see cref="Item"/> to be checked</param>
		/// <returns><see langword="true"/> if a "clicker class" item</returns>
		public static bool IsClickerItem(Item item)
		{
			return IsClickerItem(item.type);
		}

		/// <summary>
		/// Call this to check if an item is an "sfx button"
		/// </summary>
		/// <param name="item">The item to be checked</param>
		/// <returns><see langword="true"/> if an "sfx button"</returns>
		public static bool IsSFXButton(Item item)
		{
			return SFXButtons.ContainsKey(item.type);
		}

		/// <summary>
		/// Call this to check if an item type is an "sfx button"
		/// </summary>
		/// <param name="type">The item type to be checked</param>
		/// <returns><see langword="true"/> if an "sfx button"</returns>
		public static bool IsSFXButton(int type)
		{
			return SFXButtons.ContainsKey(type);
		}

		/// <summary>
		/// Call this to check if an item type is an "sfx button"
		/// </summary>
		/// <param name="type">The item type to be checked</param>
		/// <param name="playSoundAction">The <see cref="Action<int>"/> of this item for convenience, only assigned if method returns true</param>
		/// <returns><see langword="true"/> if an "sfx button"</returns>
		public static bool IsSFXButton(int type, out Action<int> playSoundAction)
		{
			return SFXButtons.TryGetValue(type, out playSoundAction);
		}

		/// <summary>
		/// Call this to check if an item type is a "clicker"
		/// </summary>
		/// <param name="type">The item type to be checked</param>
		/// <returns><see langword="true"/> if a "clicker"</returns>
		public static bool IsClickerWeapon(int type)
		{
			return ClickerWeapons.Contains(type);
		}

		/// <summary>
		/// Call this to check if an item is a "clicker"
		/// </summary>
		/// <param name="item">The <see cref="Item"/> to be checked</param>
		/// <returns><see langword="true"/> if a "clicker"</returns>
		public static bool IsClickerWeapon(Item item)
		{
			return IsClickerWeapon(item.type);
		}

		/// <summary>
		/// Call this to check if an item is a "clicker"
		/// </summary>
		/// <param name="item">The <see cref="Item"/> to be checked</param>
		/// <param name="clickerItem">The <see cref="ClickerItemCore"/> of this item for convenience, only assigned if method returns true</param>
		/// <returns><see langword="true"/> if a "clicker"</returns>
		public static bool IsClickerWeapon(Item item, out ClickerItemCore clickerItem)
		{
			clickerItem = null;
			_ = IsClickerWeapon(item) && item.TryGetGlobalItem(out clickerItem);
			return clickerItem != null;
		}

		#region Sorting
		public static void SortBy(HashSet<int> foundClickers, CatalogueSorting sorting, Mod currentMod)
		{
			Func<IEnumerable<Item>, IEnumerable<Item>> action = sorting switch
			{
				CatalogueSorting.Name_Ascending => Sort_Name_Ascending,
				CatalogueSorting.Name_Descending => Sort_Name_Descending,
				CatalogueSorting.Damage_Ascending => Sort_Damage_Ascending,
				CatalogueSorting.Damage_Descending => Sort_Damage_Descending,
				CatalogueSorting.Rarity_Ascending => Sort_Rarity_Ascending,
				CatalogueSorting.Rarity_Descending => Sort_Rarity_Descending,
				_ => throw new ArgumentOutOfRangeException(nameof(sorting), sorting, null)
			};

			SortedClickerWeapons = Sort_Internal(foundClickers, ClickerWeaponsByMod[currentMod.Name], action);
		}

		private static IEnumerable<Item> Sort_Name_Ascending(IEnumerable<Item> source) => source
			.OrderBy(x => x.Name)
		;

		private static IEnumerable<Item> Sort_Name_Descending(IEnumerable<Item> source) => source
			.OrderByDescending(x => x.Name)
		;

		private static IEnumerable<Item> Sort_Damage_Ascending(IEnumerable<Item> source) => source
			.OrderBy(x => x.damage)
			.ThenBy(x => x.Name)
		;

		private static IEnumerable<Item> Sort_Damage_Descending(IEnumerable<Item> source) => source
			.OrderBy(x => x.rare)
			.ThenBy(x => x.damage)
		;

		private static IEnumerable<Item> Sort_Rarity_Ascending(IEnumerable<Item> source) => source
			.OrderBy(x => x.rare)
			.ThenBy(x => x.damage)
		;

		private static IEnumerable<Item> Sort_Rarity_Descending(IEnumerable<Item> source) => source
			.OrderByDescending(x => x.rare)
			.ThenBy(x => x.damage)
		;

		private static List<int> Sort_Internal(HashSet<int> foundClickers, HashSet<int> currentModClickers, Func<IEnumerable<Item>, IEnumerable<Item>> action)
		{
			var preprocess = currentModClickers
				.Where(x => foundClickers.Contains(x) || !ObtainmentConditionsByClickerWeapon.TryGetValue(x, out var func) || func())
				.Select(x => ContentSamples.ItemsByType[x]);

			return action(preprocess)
			.Select(x => x.type)
			.ToList();
		}
		#endregion
	}
}
