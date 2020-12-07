using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CaptainsClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Captain's Clicker");

			ClickEffect.Bombard = ClickerSystem.RegisterClickEffect(mod, "Bombard", "Bombard", "Causes 4 cannonballs to rain from the sky", 12, new Color(255, 225, 50, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 14);

				for (int k = 0; k < 4; k++)
				{
					Vector2 startSpot = new Vector2(Main.MouseWorld.X + Main.rand.Next(-150, 151), Main.MouseWorld.Y - 500 + Main.rand.Next(-25, 26));
					Vector2 endSpot = new Vector2(Main.MouseWorld.X + Main.rand.Next(-25, 26), Main.MouseWorld.Y + Main.rand.Next(-25, 26));
					Vector2 vector = endSpot - startSpot;
					float speed = 8f + Main.rand.NextFloat(-1f, 1f);
					float mag = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
					if (mag > speed)
					{
						mag = speed / mag;
					}
					vector *= mag;
					Projectile.NewProjectile(startSpot.X, startSpot.Y, vector.X, vector.Y, ModContent.ProjectileType<CaptainsClickerPro>(), damage, knockBack, player.whoAmI, endSpot.X, endSpot.Y);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 3.15f);
			SetColor(item, new Color(255, 225, 50, 0));
			SetDust(item, 10);
			AddEffect(item, ClickEffect.Bombard);

			item.damage = 30;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 180000;
			item.rare = 4;
		}
	}
}
