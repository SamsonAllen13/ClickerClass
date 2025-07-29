using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CosmicClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			//MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Ice, projsInheritItemElements: true);
			
			//TODO - Figure out good way to handle lerp between Orange and Cyan
			ClickEffect.CosmicFlux = ClickerSystem.RegisterClickEffect(Mod, "CosmicFlux", 8, () => Color.Lerp(Color.Orange, Color.Cyan, 0.5f), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				var clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				
				switch (clickerPlayer.itemCosmicClickerStage)
				{
					case 0:
						{
							
						}
						break;
					case 1:
						{
							
						}
						break;
					case 2:
						{
							
						}
						break;
				}
				
				clickerPlayer.itemCosmicClickerStage++;
				if (clickerPlayer.itemCosmicClickerStage > 2)
				{
					clickerPlayer.itemCosmicClickerStage = 0;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6f);
			SetColor(Item, new Color(175, 220, 255));
			SetDust(Item, 221);
			AddEffect(Item, ClickEffect.CosmicFlux);

			Item.damage = 88;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}
	}
}
