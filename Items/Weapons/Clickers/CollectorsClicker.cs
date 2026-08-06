using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CollectorsClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Celestial, projsInheritItemElements: true);

			ClickEffect.Greed = ClickerSystem.RegisterClickEffect(Mod, "Greed", 1, new Color(15, 215, 215), null, preHardMode: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6.5f);
			SetColor(Item, new Color(15, 215, 215));
			SetDust(Item, 15, 255);
			AddEffect(Item, ClickEffect.Greed);

			Item.damage = 162;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(0, 25, 0, 0);
			Item.rare = ModContent.RarityType<Rarities.BloodOrangeRarity>();
		}
		
		public override void HoldItem(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().itemCollectorsClicker = true;
		}
	}
}
