using Microsoft.Xna.Framework;
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
			Tooltip.SetDefault("Click on an enemy within range and sight to damage them");
		}

		/// <summary>
		/// Call this in the inherited class as base.SetDefaults() at the start of SetDefaults. You can change the default values after it
		/// </summary>
		public override void SetDefaults()
		{
			ClickerSystem.SetClickerWeaponDefaults(item);
		}

		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> for a clicker weapon to set its color used for various things
		/// </summary>
		/// <param name="item">The clicker weapon</param>
		/// <param name="color">The color</param>
		public static void SetColor(Item item, Color color)
		{
			if (ClickerSystem.IsClickerWeapon(item, out ClickerItemCore clickerItem))
			{
				clickerItem.clickerColorItem = color;
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
		/// Call in <see cref="ModItem.SetDefaults"/> for a clicker weapon to set the amount of clicks required for an effect to trigger
		/// </summary>
		/// <param name="item">The clicker weapon</param>
		/// <param name="amount">the amount of clicks</param>
		public static void SetAmount(Item item, int amount)
		{
			if (ClickerSystem.IsClickerWeapon(item, out ClickerItemCore clickerItem))
			{
				clickerItem.itemClickerAmount = amount;
			}
		}

		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> for a clicker weapon to define its effect
		/// </summary>
		/// <param name="item">The clicker weapon</param>
		/// <param name="effect">the effect name</param>
		public static void SetEffect(Item item, string effect)
		{
			if (ClickerSystem.IsClickerWeapon(item, out ClickerItemCore clickerItem))
			{
				clickerItem.itemClickerEffect = effect;
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
