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
		internal string displayName;

		public override PrefixCategory Category => PrefixCategory.Custom;

		public ClickerPrefix() { }

		public ClickerPrefix(string displayName, float damageMult, float radiusBonus, int clickBonus, int critBonus)
		{
			this.displayName = displayName;
			this.damageMult = damageMult;
			this.radiusBonus = radiusBonus;
			this.clickBonus = clickBonus;
			this.critBonus = critBonus;
		}

		public override void SetDefaults()
		{
			DisplayName.SetDefault(displayName);
		}

		public override bool Autoload(ref string name)
		{
			if (base.Autoload(ref name))
			{
				ClickerPrefixes = new List<byte>();
				AddClickerPrefix(ClickerPrefixType.Elite, "Elite", 1.15f, 0.3f, -1, 2);
				AddClickerPrefix(ClickerPrefixType.Pro, "Pro", 1.1f, 0.2f, 0, 2);
				AddClickerPrefix(ClickerPrefixType.Amateur, "Amateur", 1f, 0.3f, -1, 0);
				AddClickerPrefix(ClickerPrefixType.Novice, "Novice", 1f, 0.2f, 0, 0);
				AddClickerPrefix(ClickerPrefixType.Laggy, "Laggy", 0.9f, -0.2f, 0, 0);
				AddClickerPrefix(ClickerPrefixType.Disconnected, "Disconnected", 0.8f, -0.3f, 1, 0);
			}
			return false;
		}

		public override void Apply(Item item)
		{
			if (item.modItem is ClickerItem clickerItem)
			{
				clickerItem.radiusBoostPrefix = radiusBonus;
				clickerItem.clickBoostPrefix = clickBonus;
			}
		}

		public override void ModifyValue(ref float valueMult) => valueMult *= 1 + radiusBonus * 0.025f;

		public override bool CanRoll(Item item)
		{
			if (item.modItem is ClickerItem)
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

		private void AddClickerPrefix(ClickerPrefixType prefixType, string displayName, float damageMult = 1f, float radiusBonus = 0f, int clickBonus = 0, int critBonus = 0)
		{
			mod.AddPrefix(prefixType.ToString(), new ClickerPrefix(displayName, damageMult, radiusBonus, clickBonus, critBonus));
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