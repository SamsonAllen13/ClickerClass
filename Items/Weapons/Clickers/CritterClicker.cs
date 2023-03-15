using ClickerClass.Utilities;
using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CritterClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Nab = ClickerSystem.RegisterClickEffect(Mod, "Nab", 1, new Color(205, 235, 255), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				for (int u = 0; u < Main.maxNPCs; u++)
				{
					NPC target = Main.npc[u];
					if (target.DistanceSQ(position) < 100 * 100)
					{
						//TODO - Figure this shit out | Is it a 1.4.4 method only or something?
						//NPC.CatchNPC(target.whoAmI, player.whoAmI);
					}
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 0.85f);
			SetColor(Item, new Color(150, 200, 255));
			SetDust(Item, 16);
			AddEffect(Item, ClickEffect.Nab);

			Item.damage = 4;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 20, 0, 0);
			Item.rare = ItemRarityID.Orange;
		}
		
		public override void HoldItem(Player player)
		{
			player.dontHurtCritters = true;
		}
		
		//TODO - Sold by zoologist after 50% bestiary 
	}
}
