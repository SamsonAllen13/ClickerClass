using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class BurningSuperDeathClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Mania = ClickerSystem.RegisterClickEffect(Mod, "Mania", 6, new Color(255, 150, 150), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				List<string> excluded = new List<string>
				{
					ClickEffect.Yoink,
					ClickEffect.Nab
				};

				List<ClickEffect> allowed = new List<ClickEffect>();

				foreach (var name in ClickerSystem.GetAllEffectNames())
				{
					if (!excluded.Contains(name) && ClickerSystem.IsClickEffect(name, out ClickEffect effect) && effect.PreHardMode)
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
			SetRadius(Item, 2.75f);
			SetColor(Item, new Color(200, 100, 0));
			SetDust(Item, 174);
			AddEffect(Item, ClickEffect.Mania);

			Item.damage = 20;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.LightRed;
		}

		public override void HoldItem(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().itemBurningSuperDeath = true;
		}
	}
}
