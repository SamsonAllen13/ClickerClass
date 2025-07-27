using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	/// <summary>
	/// Convenience class that every clicker weapon inherits from
	/// </summary>
	public abstract class ClickerWeapon : ClickerItem
	{
		public override LocalizedText Tooltip => ClickerSystem.DefaultClickerWeaponTooltipText;

		/// <summary>
		/// Call this in the inherited class as base.SetStaticDefaults() at the start of SetStaticDefaults
		/// </summary>
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

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
				
				//TODO - Clicker Catalogue - Need to somehow add Wooden Clicker too since it doesnt have a click effect
				if (!ClickerClass.mod.totalClickers.Contains(item.type))
				{
					ClickerClass.mod.totalClickers.Add(item.type);
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
