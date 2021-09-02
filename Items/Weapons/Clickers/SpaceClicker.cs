using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SpaceClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.StarStorm = ClickerSystem.RegisterClickEffect(mod, "StarStorm", null, null, 6, new Color(175, 75, 255), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 42);

				for (int k = 0; k < 3; k++)
				{
					Vector2 startSpot = new Vector2(Main.MouseWorld.X + Main.rand.Next(-100, 101), Main.MouseWorld.Y - 500 + Main.rand.Next(-25, 26));
					Vector2 endSpot = new Vector2(Main.MouseWorld.X + Main.rand.Next(-25, 26), Main.MouseWorld.Y + Main.rand.Next(-25, 26));
					Vector2 vector = endSpot - startSpot;
					float speed = 5f;
					float mag = vector.Length();
					if (mag > speed)
					{
						mag = speed / mag;
					}
					vector *= mag;
					Projectile.NewProjectile(startSpot, vector, ModContent.ProjectileType<SpaceClickerPro>(), (int)(damage * 0.75f), knockBack, player.whoAmI, endSpot.X, endSpot.Y);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.25f);
			SetColor(item, new Color(175, 125, 125));
			SetDust(item, 174);
			AddEffect(item, ClickEffect.StarStorm);

			item.damage = 7;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 15000;
			item.rare = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MeteoriteBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
