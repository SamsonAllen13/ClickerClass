using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class WitchClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.WildMagic = ClickerSystem.RegisterClickEffect(Mod, "WildMagic", null, null, 6, new Color(175, 75, 255), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				List<string> excluded = new List<string>
				{
					ClickEffect.WildMagic,
					ClickEffect.Conqueror,
					ClickEffect.AutoClick
				};

				List<string> allowed = new List<string>();

				foreach (var name in ClickerSystem.GetAllEffectNames())
				{
					if (!excluded.Contains(name))
					{
						allowed.Add(name);
					}
				}

				if (allowed.Count == 0) return;

				string random = Main.rand.Next(allowed);
				if (ClickerSystem.IsClickEffect(random, out ClickEffect effect))
				{
					effect.Action?.Invoke(player, source, position, type, damage, knockBack);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6f);
			SetColor(Item, new Color(175, 75, 255));
			SetDust(Item, 173);
			AddEffect(Item, ClickEffect.WildMagic);

			Item.damage = 64;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 500000;
			Item.rare = 8;
		}
	}
}
