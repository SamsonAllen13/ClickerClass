using ClickerClass.Items;
using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Localization;
using ClickerClass.Items.Misc;

namespace ClickerClass
{
	/// <summary>
	/// Manages registering clicker class related content and provides basic methods to check for content being clicker class related
	/// </summary>
	public class ClickerSystem : ModSystem
	{
		//Clientside only hence why player instance to play the sound at is not necessary
		/// <summary>
		/// The method used to play a sound
		/// </summary>
		/// <param name="stack">Usually used to control the volume by multiplying with 0.5f, ranges from 1 to <see cref="SFXButtonBase.StackAmount"/></param>
		public delegate void SFXButtonSoundDelegate(int stack);

		/// <summary>
		/// To prevent certain methods being called when they shouldn't
		/// </summary>
		internal static bool FinalizedRegisterCompat { get; private set; }

		internal static LocalizedText UnknownText { get; private set; }

		internal static LocalizedText DefaultClickerWeaponTooltipText { get; private set; }

		private static HashSet<int> ClickerItems { get; set; }

		private static Dictionary<int, string> ClickerWeaponBorderTexture { get; set; }

		private static HashSet<int> ClickerWeapons { get; set; }

		private static Dictionary<int, SFXButtonSoundDelegate> SFXButtons { get; set; }

		private static HashSet<int> ClickerWeaponProjectiles { get; set; }

		private static HashSet<int> ClickerProjectiles { get; set; }

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
			SFXButtons = new Dictionary<int, SFXButtonSoundDelegate>();
			ClickerProjectiles = new HashSet<int>();
			ClickerWeaponProjectiles = new HashSet<int>();
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
			SFXButtons = null;
			ClickerProjectiles = null;
			ClickerWeaponProjectiles = null;
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
			FinalizedRegisterCompat = true;
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
		/// Call this in <see cref="ModType.SetStaticDefaults"/> to register this projectile into the "clicker class" category
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
			if (!ClickerProjectiles.Contains(type))
			{
				ClickerProjectiles.Add(type);
			}
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
			if (!ClickerWeaponProjectiles.Contains(type))
			{
				ClickerWeaponProjectiles.Add(type);
			}
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
			if (!ClickerItems.Contains(type))
			{
				ClickerItems.Add(type);
			}
		}

		/// <summary>
		/// Call this in <see cref="ModType.SetStaticDefaults"/> to register this weapon into the "clicker class" category as a "clicker".<br/>
		/// Do not call <see cref="RegisterClickerItem"/> with it as this method does this already by itself
		/// </summary>
		/// <param name="modItem">The <see cref="ModItem"/> that is to be registered</param>
		/// <param name="borderTexture">The path to the border texture (optional)</param>
		/// <exception cref="InvalidOperationException"/>
		public static void RegisterClickerWeapon(ModItem modItem, string borderTexture = null)
		{
			if (FinalizedRegisterCompat)
			{
				throw new InvalidOperationException("Tried to register a clicker weapon at the wrong time, do so in ModItem.SetStaticDefaults");
			}
			RegisterClickerItem(modItem);
			int type = modItem.Item.type;
			if (!ClickerWeapons.Contains(type))
			{
				ClickerWeapons.Add(type);
				if (borderTexture != null)
				{
					if (ModContent.HasAsset(borderTexture))
					{
						if (!ClickerWeaponBorderTexture.ContainsKey(type))
						{
							ClickerWeaponBorderTexture.Add(type, borderTexture);
						}
					}
					else
					{
						ClickerClass.mod.Logger.Info($"Border texture for {modItem.Name} not found: {borderTexture}");
					}
				}
			}
		}

		/// <summary>
		/// Call this in <see cref="ModType.SetStaticDefaults"/> to register this item into the "sfx button" category.<br/>
		/// It will automatically contribute to the active "sfx buttons" when in the inventory<br/>
		/// Do not call <see cref="RegisterClickerItem"/> with it as this method does this already by itself
		/// </summary>
		/// <param name="modItem">The <see cref="ModItem"/> that is to be registered</param>
		/// <param name="playSoundAction">The <see cref="SFXButtonSoundDelegate"/> that will play the sound</param>
		/// <exception cref="InvalidOperationException"/>
		public static void RegisterSFXButton(ModItem modItem, SFXButtonSoundDelegate playSoundAction)
		{
			if (FinalizedRegisterCompat)
			{
				throw new InvalidOperationException("Tried to register an sfx button at the wrong time, do so in ModItem.SetStaticDefaults");
			}
			RegisterClickerItem(modItem);
			int type = modItem.Item.type;
			if (!SFXButtons.ContainsKey(type))
			{
				SFXButtons.Add(type, playSoundAction);
			}
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
		/// <param name="playSoundAction">The <see cref="SFXButtonSoundDelegate"/> of this item for convenience, only assigned if method returns true</param>
		/// <returns><see langword="true"/> if an "sfx button"</returns>
		public static bool IsSFXButton(int type, out SFXButtonSoundDelegate playSoundAction)
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
	}
}
