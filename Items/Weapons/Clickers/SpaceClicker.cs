using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SpaceClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.StarStorm = ClickerSystem.RegisterClickEffect(Mod, "StarStorm", null, null, 6, new Color(175, 75, 255), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 42);

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
						vector *= mag;
					}
					Projectile.NewProjectile(source, startSpot, vector, ModContent.ProjectileType<SpaceClickerPro>(), (int)(damage * 0.75f), knockBack, player.whoAmI, endSpot.X, endSpot.Y);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.25f);
			SetColor(Item, new Color(175, 125, 125));
			SetDust(Item, 174);
			AddEffect(Item, ClickEffect.StarStorm);

			Item.damage = 10;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 15000;
			Item.rare = 1;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.MeteoriteBar, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
