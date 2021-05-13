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

			ClickEffect.PetalStorm = ClickerSystem.RegisterClickEffect(mod, "PetalStorm", null, null, 10, new Color(255, 150, 255), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
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
					float mag = vector.Length();
					if (mag > speed)
					{
						mag = speed / mag;
					}
					vector *= mag;

					int orichalcum = ModContent.ProjectileType<OrichaclumClickerPro>();
					Projectile.NewProjectile(startSpot, vector, orichalcum, (int)(damage * 0.5f), 0f, player.whoAmI, Main.rand.Next(Main.projFrames[orichalcum]), 0f);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 3f);
			SetColor(item, new Color(255, 150, 255));
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
