using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class WitchClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.WildMagic = ClickerSystem.RegisterClickEffect(Mod, "WildMagic", 6, new Color(175, 75, 255), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				List<string> excluded = new List<string>
				{
					ClickEffect.WildMagic,
					ClickEffect.Mania,
					ClickEffect.Conqueror,
					ClickEffect.AutoClick,
					ClickEffect.PhaseReach,
					ClickEffect.Bold,
					ClickEffect.Yoink,
					ClickEffect.Nab
				};

				List<ClickEffect> allowed = new List<ClickEffect>();

				foreach (var name in ClickerSystem.GetAllEffectNames())
				{
					if (!excluded.Contains(name) && ClickerSystem.IsClickEffect(name, out ClickEffect effect))
					{
						allowed.Add(effect);
					}
				}

				if (allowed.Count == 0) return;

				ClickEffect random = Main.rand.Next(allowed);
				random.Action?.Invoke(player, source, position, type, damage, knockBack);
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
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}
	}
}
