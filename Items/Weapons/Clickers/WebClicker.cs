using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class WebClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.WebSplash = ClickerSystem.RegisterClickEffect(Mod, "WebSplash", 10, new Color(190, 190, 175), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Vector2 mouse = position;

				int index = -1;
				float maxDistSQ = 400f * 400f;
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy())
					{
						float distSQ = npc.DistanceSQ(mouse);
						if (distSQ < maxDistSQ && Collision.CanHit(mouse, 1, 1, npc.Center, 1, 1))
						{
							maxDistSQ = distSQ;
							index = i;
						}
					}
				}
				if (index != -1)
				{
					Vector2 vector = Main.npc[index].Center - mouse;
					float speed = 6f;
					float mag = vector.Length();
					if (mag > speed)
					{
						mag = speed / mag;
						vector *= mag;
					}
					Projectile.NewProjectile(source, position, vector, ModContent.ProjectileType<WebClickerPro>(), damage, knockBack, player.whoAmI);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.85f);
			SetColor(Item, new Color(150, 80, 50));
			SetDust(Item, 148);
			AddEffect(Item, ClickEffect.WebSplash);

			Item.damage = 22;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 2, 40, 0);
			Item.rare = ItemRarityID.LightRed;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.SpiderFang, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
