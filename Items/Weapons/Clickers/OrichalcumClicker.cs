using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class OrichalcumClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Orichalcum Clicker");

			ClickEffect.PetalStorm = ClickerSystem.RegisterClickEffect(mod, "PetalStorm", "Petal Storm", "Causes 5 petal projectiles to be summoned near the player and shoot across the screen", 10, new Color(255, 150, 255, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 24);

				for (int k = 0; k < 5; k++)
				{
					float xChoice = Main.rand.Next(-100, 101);
					float yChoice = Main.rand.Next(-100, 101);
					xChoice += xChoice > 0 ? 300 : -300;
					yChoice += yChoice > 0 ? 300 : -300;
					Vector2 startSpot = new Vector2(Main.MouseWorld.X + xChoice, Main.MouseWorld.Y + yChoice);
					Vector2 endSpot = new Vector2(Main.MouseWorld.X + Main.rand.Next(-10, 11), Main.MouseWorld.Y + Main.rand.Next(-10, 11));
					Vector2 vector = endSpot - startSpot;
					float speed = 4f;
					float mag = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
					if (mag > speed)
					{
						mag = speed / mag;
					}
					vector *= mag;
					Projectile.NewProjectile(startSpot.X, startSpot.Y, vector.X, vector.Y, ModContent.ProjectileType<OrichaclumClickerPro>(), (int)(damage * 0.5f), 0f, player.whoAmI, Main.rand.Next(3), 0f);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 3f);
			SetColor(item, new Color(255, 150, 255, 0));
			SetDust(item, 145);
			AddEffect(item, ClickEffect.PetalStorm);

			item.damage = 28;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 126500;
			item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.OrichalcumBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
