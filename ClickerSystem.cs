using ClickerClass.Items;
using ClickerClass.Projectiles;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass
{
	/// <summary>
	/// Manages registering clicker class related content and provides basic methods to check for content being clicker class related
	/// </summary>
	public static class ClickerSystem
	{
		private static HashSet<int> ClickerItems { get; set; }

		private static HashSet<int> ClickerWeapons { get; set; }

		private static HashSet<int> ClickerProjectiles { get; set; }

		internal static void Load()
		{
			ClickerItems = new HashSet<int>();
			ClickerWeapons = new HashSet<int>();
			ClickerProjectiles = new HashSet<int>();
		}

		internal static void Unload()
		{
			ClickerItems = null;
			ClickerWeapons = null;
			ClickerProjectiles = null;
		}

		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> to set important default fields for a clicker weapon. Set fields:
		/// useTime, useAnimation, useStyle, holdStyle, noMelee, shoot, shootSpeed.
		/// Only change them afterwards if you know what you are doing!
		/// </summary>
		/// <param name="item">The <see cref="Item"/> to set the defaults for</param>
		public static void SetClickerWeaponDefaults(Item item)
		{
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.holdStyle = 3;
			item.noMelee = true;
			item.shoot = ModContent.ProjectileType<ClickDamage>();
			item.shootSpeed = 1f;
		}

		/// <summary>
		/// Call this in <see cref="ModProjectile.SetStaticDefaults"/> to register this projectile into the "clicker class" category
		/// </summary>
		/// <param name="modProj">The <see cref="ModProjectile"/> that is to be registered</param>
		public static void RegisterClickerProjectile(ModProjectile modProj)
		{
			if (ClickerClass.finalizedRegisterCompat)
			{
				throw new Exception("Tried to register a clicker projectile at the wrong time, do so in ModProjectile.SetStaticDefaults");
			}
			int type = modProj.projectile.type;
			if (!ClickerProjectiles.Contains(type))
			{
				ClickerProjectiles.Add(type);
			}
		}

		/// <summary>
		/// Call this in <see cref="ModItem.SetStaticDefaults"/> to register this item into the "clicker class" category
		/// </summary>
		/// <param name="modItem">The <see cref="ModItem"/> that is to be registered</param>
		public static void RegisterClickerItem(ModItem modItem)
		{
			if (ClickerClass.finalizedRegisterCompat)
			{
				throw new Exception("Tried to register a clicker item at the wrong time, do so in ModItem.SetStaticDefaults");
			}
			int type = modItem.item.type;
			if (!ClickerItems.Contains(type))
			{
				ClickerItems.Add(type);
			}
		}

		/// <summary>
		/// Call this in <see cref="ModItem.SetStaticDefaults"/> to register this weapon into the "clicker class" category as a "clicker".
		/// You can change the default tooltip after it.
		/// Do not call <see cref="RegisterClickerItem"/> with it as this method does this already by itself
		/// </summary>
		/// <param name="modItem">The <see cref="ModItem"/> that is to be registered</param>
		public static void RegisterClickerWeapon(ModItem modItem)
		{
			if (ClickerClass.finalizedRegisterCompat)
			{
				throw new Exception("Tried to register a clicker weapon at the wrong time, do so in ModItem.SetStaticDefaults");
			}
			RegisterClickerItem(modItem);
			int type = modItem.item.type;
			if (!ClickerWeapons.Contains(type))
			{
				ClickerWeapons.Add(type);
			}
			modItem.Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
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
