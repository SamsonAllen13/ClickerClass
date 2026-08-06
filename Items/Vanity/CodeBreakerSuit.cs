using ClickerClass.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Vanity
{
	[AutoloadEquip(EquipType.Body)]
	public class CodeBreakerSuit : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			
			if (!Main.dedServ)
			{
				BodyGlowmaskPlayer.RegisterData(Item.bodySlot, () => new Color(255, 255, 255, 125) * 0.85f);
			}
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.buyPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.vanity = true;
		}
	}
}
