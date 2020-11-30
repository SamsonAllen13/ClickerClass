using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class WitchClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Witch Clicker");

			ClickEffect.WildMagic = ClickerSystem.RegisterClickEffect(mod, "WildMagic", "Wild Magic", "Randomly acts as any possible click effect", 6, new Color(175, 75, 255, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				List<string> excluded = new List<string>
				{
					ClickEffect.WildMagic,
					ClickEffect.Conqueror
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
					effect.Action?.Invoke(player, position, type, damage, knockBack);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 6f);
			SetColor(item, new Color(175, 75, 255, 0));
			SetDust(item, 173);
			AddEffect(item, ClickEffect.WildMagic);

			item.damage = 78;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 500000;
			item.rare = 8;
		}
	}
}
