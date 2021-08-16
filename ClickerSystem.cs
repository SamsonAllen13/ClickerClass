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

namespace ClickerClass
{
	/// <summary>
	/// Manages registering clicker class related content and provides basic methods to check for content being clicker class related
	/// </summary>
	public static class ClickerSystem
	{
		private static HashSet<int> ClickerItems { get; set; }

		private static Dictionary<int, string> ClickerWeaponBorderTexture { get; set; }

		private static HashSet<int> ClickerWeapons { get; set; }

		private static HashSet<int> ClickerProjectiles { get; set; }

		/// <summary>
		/// A dictionary containing registered (!) ClickEffects. When "creating" new ones to assign to something, it clones it from this
		/// </summary>
		private static Dictionary<string, ClickEffect> ClickEffectsByName { get; set; }

		internal static void Load()
		{
			ClickerItems = new HashSet<int>();
			ClickerWeaponBorderTexture = new Dictionary<int, string>();
			ClickerWeapons = new HashSet<int>();
			ClickerProjectiles = new HashSet<int>();
			ClickEffectsByName = new Dictionary<string, ClickEffect>();
		}

		internal static void Unload()
		{
			ClickerItems = null;
			ClickerWeaponBorderTexture?.Clear();
			ClickerWeaponBorderTexture = null;
			ClickerWeapons = null;
			ClickerProjectiles = null;
			ClickEffectsByName?.Clear();
			ClickEffectsByName = null;
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
		/// "Mod": The mod the effect belongs to (string).
		/// | "InternalName": The internal name (string).
		/// | "UniqueName": The unique name (string).
		/// | "DisplayName": The displayed name (string).
		/// | "Description": The description (string).
		/// | "Amount": The amount of clicks to trigger the effect (int).
		/// | "Color": The color (Color).
		/// | "Action": The method ran when triggered (Action[Player, Vector2, int, int, float]).
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
		/// Mod Compat way of getting obsolete effect names. Can return null if not found
		/// </summary>
		/// <param name="oldName">The old display name</param>
		/// <param name="internalName">The associated internal name</param>
		/// <returns><see langword="true"/> if exists</returns>
		internal static bool GetNewNameFromOldDisplayName(string oldName, out string internalName)
		{
			var allEffects = GetAllEffects();
			var found = allEffects.FirstOrDefault(e => e.Value.DisplayName == oldName);
			internalName = found.Key;
			return internalName != null;
		}

		/// <summary>
		/// Call this in <see cref="Mod.PostSetupContent"/> or <see cref="ModItem.SetStaticDefaults"/> to register this click effect
		/// </summary>
		/// <param name="mod">The mod this effect belongs to. ONLY USE YOUR OWN MOD INSTANCE FOR THIS!</param>
		/// <param name="internalName">The internal name of the effect. Turns into the unique name combined with the associated mod</param>
		/// <param name="displayName">The name of the effect, null if you use lang keys (Defaults to ClickEffect.[internalName].Name)</param>
		/// <param name="description">The basic description of the effect, string.Empty for none, null if you use lang keys (Defaults to ClickEffect.[internalName].Description)</param>
		/// <param name="amount">The amount of clicks required to trigger the effect</param>
		/// <param name="action">The method that runs when the effect is triggered</param>
		/// <returns>The unique identifier</returns>
		/// <exception cref="InvalidOperationException"/>
		public static string RegisterClickEffect(Mod mod, string internalName, string displayName, string description, int amount, Color color, Action<Player, ProjectileSource_Item_WithAmmo, Vector2, int, int, float> action)
		{
			if (ClickerClass.finalizedRegisterCompat)
			{
				throw new InvalidOperationException("Tried to register a click effect at the wrong time, do so in Mod.PostSetupContent or ModItem.SetStaticDefaults");
			}
			if (string.IsNullOrEmpty(internalName))
			{
				throw new InvalidOperationException($"internalName is either null or empty. Give it a proper value");
			}

			string uniqueName = UniqueEffectName(mod, internalName);

			ClickEffect effect = new ClickEffect(mod, internalName, displayName, description, amount, color, action);

			if (!IsClickEffect(uniqueName))
			{
				ClickEffectsByName.Add(uniqueName, effect);
				return uniqueName;
			}
			else
			{
				throw new InvalidOperationException($"The effect '{uniqueName}' has already been registered, duplicate detected");
			}
		}

		internal static void FinalizeLocalization()
		{
			// Modded Localization keys are initialized before AddRecipes, so we need to do this late.
			foreach (var effect in ClickEffectsByName.Values)
			{
				effect.FinalizeLocalization();
			}
		}

		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> to set important default fields for a clicker weapon. Set fields:
		/// DamageType, crit, useTime, useAnimation, useStyle, holdStyle, noMelee, shoot, shootSpeed.
		/// Only change them afterwards if you know what you are doing!
		/// </summary>
		/// <param name="item">The <see cref="Item"/> to set the defaults for</param>
		public static void SetClickerWeaponDefaults(Item item)
		{
			item.DamageType = ModContent.GetInstance<ClickerDamage>();
			item.crit = 4;
			item.useTime = 2;
			item.useAnimation = 2;
			item.useStyle = ItemUseStyleID.Shoot;
			item.holdStyle = 3;
			item.noMelee = true;
			item.shoot = ModContent.ProjectileType<ClickDamage>();
			item.shootSpeed = 1f;
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
		/// Call this in <see cref="ModProjectile.SetStaticDefaults"/> to register this projectile into the "clicker class" category
		/// </summary>
		/// <param name="modProj">The <see cref="ModProjectile"/> that is to be registered</param>
		/// <exception cref="InvalidOperationException"/>
		public static void RegisterClickerProjectile(ModProjectile modProj)
		{
			if (ClickerClass.finalizedRegisterCompat)
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
		/// Call this in <see cref="ModItem.SetStaticDefaults"/> to register this item into the "clicker class" category
		/// </summary>
		/// <param name="modItem">The <see cref="ModItem"/> that is to be registered</param>
		/// <exception cref="InvalidOperationException"/>
		public static void RegisterClickerItem(ModItem modItem)
		{
			if (ClickerClass.finalizedRegisterCompat)
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
		/// Call this in <see cref="ModItem.SetStaticDefaults"/> to register this weapon into the "clicker class" category as a "clicker".
		/// You can change the default tooltip BEFORE it.
		/// Do not call <see cref="RegisterClickerItem"/> with it as this method does this already by itself
		/// </summary>
		/// <param name="modItem">The <see cref="ModItem"/> that is to be registered</param>
		/// <param name="borderTexture">The path to the border texture (optional)</param>
		/// <exception cref="InvalidOperationException"/>
		public static void RegisterClickerWeapon(ModItem modItem, string borderTexture = null)
		{
			if (ClickerClass.finalizedRegisterCompat)
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

			if (modItem.Tooltip.GetDefault() == null)
			{
				modItem.Tooltip.SetDefault("{$Mods.ClickerClass.Common.Tooltips.Clicker}");
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
		/// <param name="type">The item type to be checked</param>
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
		/// Call this to check if an item belongs to the "clicker class" category
		/// </summary>
		/// <param name="item">The <see cref="Item"/> to be checked</param>
		/// <param name="clickerItem">The <see cref="ClickerItemCore"/> of this item for convenience, only assigned if method returns true</param>
		/// <returns><see langword="true"/> if a "clicker class" item</returns>
		public static bool IsClickerItem(Item item, out ClickerItemCore clickerItem)
		{
			bool ret = IsClickerItem(item);
			clickerItem = null;
			if (ret)
			{
				clickerItem = item.GetGlobalItem<ClickerItemCore>();
			}
			return ret;
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
			bool ret = IsClickerWeapon(item);
			clickerItem = null;
			if (ret)
			{
				clickerItem = item.GetGlobalItem<ClickerItemCore>();
			}
			return ret;
		}
	}
}
