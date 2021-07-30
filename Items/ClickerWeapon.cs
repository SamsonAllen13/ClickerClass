using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	/// <summary>
	/// Convenience class that every clicker weapon inherits from
	/// </summary>
	public abstract class ClickerWeapon : ClickerItem
	{
		/// <summary>
		/// Call this in the inherited class as base.SetStaticDefaults() at the start of SetStaticDefaults. You can change the default tooltip after it
		/// </summary>
		public override void SetStaticDefaults()
		{
			ClickerSystem.RegisterClickerWeapon(this);
		}

		/// <summary>
		/// Call this in the inherited class as base.SetDefaults() at the start of SetDefaults. You can change the default values after it
		/// </summary>
		public override void SetDefaults()
		{
			ClickerSystem.SetClickerWeaponDefaults(Item);
		}

		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> for a clicker weapon to set its radius color
		/// </summary>
		/// <param name="item">The clicker weapon</param>
		/// <param name="color">The color</param>
		public static void SetColor(Item item, Color color)
		{
			if (ClickerSystem.IsClickerWeapon(item, out ClickerItemCore clickerItem))
			{
				clickerItem.clickerRadiusColor = color;
			}
		}

		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> for a clicker weapon to set its specific radius increase (1f means 100 pixel)
		/// </summary>
		/// <param name="item">The clicker weapon</param>
		/// <param name="radius">The additional radius</param>
		public static void SetRadius(Item item, float radius)
		{
			if (ClickerSystem.IsClickerWeapon(item, out ClickerItemCore clickerItem))
			{
				clickerItem.radiusBoost = radius;
			}
		}

		[Obsolete("Use AddEffect instead", true)]
		public static void SetAmount(Item item, int amount)
		{
			//Nothing
		}

		[Obsolete("Use AddEffect instead", true)]
		public static void SetEffect(Item item, string effect)
		{
			AddEffect(item, effect);
		}

		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> for a clicker weapon to add an effect to it
		/// </summary>
		/// <param name="item">The clicker weapon</param>
		/// <param name="effect">the effect name</param>
		public static void AddEffect(Item item, string effect)
		{
			AddEffect(item, new List<string> { effect });
		}

		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> for a clicker weapon to add effects to it
		/// </summary>
		/// <param name="item">The clicker weapon</param>
		/// <param name="effects">the effect names</param>
		public static void AddEffect(Item item, IEnumerable<string> effects)
		{
			if (ClickerSystem.IsClickerWeapon(item, out ClickerItemCore clickerItem))
			{
				//Check against already added effects
				List<string> list = clickerItem.itemClickEffects;
				foreach (var name in effects)
				{
					if (!string.IsNullOrEmpty(name) && !list.Contains(name))
					{
						if (ClickerSystem.IsClickEffect(name))
						{
							list.Add(name);
						}
					}
				}
			}
		}

		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> for a clicker weapon to set its dust type when it's used
		/// </summary>
		/// <param name="item">The clicker weapon</param>
		/// <param name="type">the dust type</param>
		public static void SetDust(Item item, int type)
		{
			if (ClickerSystem.IsClickerWeapon(item, out ClickerItemCore clickerItem))
			{
				clickerItem.clickerDustColor = type;
			}
		}
	}
}
