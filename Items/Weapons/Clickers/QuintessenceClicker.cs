using ClickerClass.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;
using System.Collections.Generic;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class QuintessenceClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Evolve = ClickerSystem.RegisterClickEffect(Mod, "Evolve", null, null, 10, new Color(255, 150, 150), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.75f);
			SetColor(Item, new Color(200, 100, 0));
			SetDust(Item, 174);
			AddEffect(Item, ClickEffect.Evolve);

			Item.damage = 20;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Pink;
		}
		
		//TODO - Shimmer a wooden clicker to craft
	}
}
