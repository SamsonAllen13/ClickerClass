using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class BurrowingClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Shadow, projsInheritItemElements: true);

			ClickEffect.Burrow = ClickerSystem.RegisterClickEffect(Mod, "Burrow", 1, new Color(165, 140, 140), null, preHardMode: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.15f);
			SetColor(Item, new Color(185, 150, 160));
			SetDust(Item, 18);
			AddEffect(Item, ClickEffect.Burrow);

			Item.damage = 8;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 0, 50, 0);
			Item.rare = ItemRarityID.Blue;
		}
	}
}
