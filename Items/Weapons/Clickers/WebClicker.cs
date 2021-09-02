using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class WebClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.WebSplash = ClickerSystem.RegisterClickEffect(mod, "WebSplash", null, null, 10, new Color(190, 190, 175), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Vector2 mouse = Main.MouseWorld;

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
					}
					vector *= mag;
					Projectile.NewProjectile(Main.MouseWorld, vector, ModContent.ProjectileType<WebClickerPro>(), damage, knockBack, player.whoAmI);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.85f);
			SetColor(item, new Color(150, 80, 50));
			SetDust(item, 148);
			AddEffect(item, ClickEffect.WebSplash);

			item.damage = 22;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 120000;
			item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpiderFang, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
