using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

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
					if (target.active && target.catchItem > 0 && target.DistanceSQ(position) < 100 * 100)
					{
						NPC.CheckCatchNPC(target, target.Hitbox, player.HeldItem, player);
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
	}
}
