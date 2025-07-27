using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class WatchfulClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Blood, projsInheritItemElements: true);

			ClickEffect.Peekaboo = ClickerSystem.RegisterClickEffect(Mod, "Peekaboo", 1, new Color(255, 230, 230), null, preHardMode: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.4f);
			SetColor(Item, new Color(255, 255, 255));
			SetDust(Item, 5);
			AddEffect(Item, ClickEffect.Peekaboo);

			Item.damage = 7;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 0, 45, 0);
			Item.rare = ItemRarityID.Blue;
		}
	}
}
