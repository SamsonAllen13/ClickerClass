using ClickerClass.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Prefixes
{
	public class ClickerPrefix : ModPrefix
	{
		internal static List<byte> ClickerPrefixes;
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

		public override bool Autoload(ref string name)
		{
			if (base.Autoload(ref name))
			{
				ClickerPrefixes = new List<byte>();
				AddClickerPrefix(ClickerPrefixType.Elite, 1.15f, 0.3f, -1, 2);
				AddClickerPrefix(ClickerPrefixType.Pro, 1.1f, 0.2f, 0, 2);
				AddClickerPrefix(ClickerPrefixType.Amateur, 1f, 0.3f, -1, 0);
				AddClickerPrefix(ClickerPrefixType.Novice, 1f, 0.2f, 0, 0);
				AddClickerPrefix(ClickerPrefixType.Laggy, 0.9f, -0.2f, 0, 0);
				AddClickerPrefix(ClickerPrefixType.Disconnected, 0.8f, -0.3f, 1, 0);
			}
			return false;
		}

		public override void Apply(Item item)
		{
			if (ClickerSystem.IsClickerWeapon(item, out ClickerItemCore clickerItem))
			{
				clickerItem.radiusBoostPrefix = radiusBonus;
				clickerItem.clickBoostPrefix = clickBonus;
			}
		}

		public override void ModifyValue(ref float valueMult) => valueMult *= 1 + radiusBonus * 0.025f;

		public override bool CanRoll(Item item)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				return item.maxStack == 1 && item.damage > 0 && item.useStyle > 0;
			}
			return false;
		}

		public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
		{
			damageMult = this.damageMult;
			critBonus = this.critBonus;
		}

		private void AddClickerPrefix(ClickerPrefixType prefixType, float damageMult = 1f, float radiusBonus = 0f, int clickBonus = 0, int critBonus = 0)
		{
			mod.AddPrefix(prefixType.ToString(), new ClickerPrefix(damageMult, radiusBonus, clickBonus, critBonus));
			ClickerPrefixes.Add(mod.GetPrefix(prefixType.ToString()).Type);
		}
	}

	public enum ClickerPrefixType : byte
	{
		Elite,
		Pro,
		Amateur,
		Novice,
		Laggy,
		Disconnected
	}
}