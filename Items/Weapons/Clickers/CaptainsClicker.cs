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
	public class CaptainsClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Bombard = ClickerSystem.RegisterClickEffect(Mod, "Bombard", null, null, 12, new Color(255, 225, 50), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(SoundID.Item, (int)position.X, (int)position.Y, 14);

				for (int k = 0; k < 4; k++)
				{
					Vector2 startSpot = new Vector2(position.X + Main.rand.Next(-150, 151), position.Y - 500 + Main.rand.Next(-25, 26));
					Vector2 endSpot = new Vector2(position.X + Main.rand.Next(-25, 26), position.Y + Main.rand.Next(-25, 26));
					Vector2 vector = endSpot - startSpot;
					float speed = 8f + Main.rand.NextFloat(-1f, 1f);
					float mag = vector.Length();
					if (mag > speed)
					{
						mag = speed / mag;
						vector *= mag;
					}
					Projectile.NewProjectile(source, startSpot, vector, ModContent.ProjectileType<CaptainsClickerPro>(), damage, knockBack, player.whoAmI, endSpot.X, endSpot.Y);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.15f);
			SetColor(Item, new Color(255, 225, 50));
			SetDust(Item, 10);
			AddEffect(Item, ClickEffect.Bombard);

			Item.damage = 26;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 180000;
			Item.rare = 4;
		}
	}
}
