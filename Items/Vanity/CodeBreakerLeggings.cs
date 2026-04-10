using ClickerClass.DrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Vanity
{
	[AutoloadEquip(EquipType.Legs)]
	public class CodeBreakerLeggings : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			
			if (!Main.dedServ)
			{
				LegsLayer.RegisterData(Item.legSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>(Texture + "_Legs_Glow"),
					Color = (PlayerDrawSet drawInfo) => new Color(255, 255, 255, 125) * 0.85f
				});
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
