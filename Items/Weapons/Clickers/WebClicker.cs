using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
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
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 17);
				Vector2 pos = Main.MouseWorld;

				int index = -1;
				for (int i = 0; i < 200; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.active && npc.CanBeChasedBy() && Vector2.DistanceSquared(pos, npc.Center) < 400f * 400f && Collision.CanHit(pos, 1, 1, npc.Center, 1, 1))
					{
						index = i;
					}
				}
				if (index != -1)
				{
					Vector2 vector = Main.npc[index].Center - pos;
					float speed = 6f;
					float mag = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
					if (mag > speed)
					{
						mag = speed / mag;
					}
					vector *= mag;
					Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, vector.X, vector.Y, ModContent.ProjectileType<WebClickerPro>(), damage, knockBack, player.whoAmI);
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

			item.damage = 25;
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
