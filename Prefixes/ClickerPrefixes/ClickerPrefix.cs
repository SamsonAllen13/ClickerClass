using ClickerClass.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Prefixes.ClickerPrefixes
{
	/// <summary>
	/// Classes inheriting this need a default constructor using the base constructor
	/// </summary>
	public abstract class ClickerPrefix : ModPrefix
	{
		/// <summary>
		/// List of all registered clicker prefix types
		/// </summary>
		internal static List<int> ClickerPrefixes;

		internal float damageMult = 1f;
		internal float radiusBonus = 0;
		internal int clickBonus = 0;
		internal int critBonus = 0;

		public override PrefixCategory Category => PrefixCategory.Custom;

		public ClickerPrefix() { }

		public ClickerPrefix(float damageMult, float radiusBonus, int clickBonus, int critBonus)
		{
			this.damageMult = damageMult;
			this.radiusBonus = radiusBonus;
			this.clickBonus = clickBonus;
			this.critBonus = critBonus;
		}

		public override void Load()
		{
			ClickerPrefixes = new List<int>();
		}

		public override void Unload()
		{
			ClickerPrefixes = null;
		}

		public override void SetStaticDefaults()
		{
			ClickerPrefixes.Add(Type);
		}

		public override void Apply(Item item)
		{
			if (ClickerSystem.IsClickerWeapon(item, out ClickerItemCore clickerItem))
			{
				clickerItem.radiusBoostPrefix = radiusBonus;
				clickerItem.clickBoostPrefix = clickBonus;
			}
		}

		public override void ModifyValue(ref float valueMult)
		{
			valueMult *= 1 + radiusBonus * 0.025f;
		}

		public static bool DoConditionsApply(Item item)
		{
			return ClickerSystem.IsClickerWeapon(item) && item.maxStack == 1 && item.damage > 0 && item.useStyle > 0;
		}

		public override bool CanRoll(Item item)
		{
			return DoConditionsApply(item);
		}

		public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
		{
			damageMult = this.damageMult;
			critBonus = this.critBonus;
		}
	}
}
