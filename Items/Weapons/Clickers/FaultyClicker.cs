using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class FaultyClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Fritz = ClickerSystem.RegisterClickEffect(Mod, "Fritz", null, null, 3, new Color(100, 100, 100), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				var clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				
				int radius = (int)(95 * clickerPlayer.clickerRadius);
				if (radius > 350)
				{
					radius = 350;
				}

				Vector2 offset = player.Center;
				int tries = 0;
				do
				{
					offset = Vector2.UnitY.RotatedByRandom(MathHelper.TwoPi);
					offset *= radius * Main.rand.NextFloat();
					offset += player.Center;
					tries++;
				}
				while (tries < 100 && !Collision.CanHit(new Vector2(player.Center.X, player.Center.Y - 12), 1, 1, offset, 1, 1));

				if (tries >= 100)
				{
					return;
				}

				Projectile.NewProjectile(source, offset, Vector2.Zero, ModContent.ProjectileType<FaultyClickerPro>(), (int)(damage * 2f), 0f, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1f);
			SetColor(Item, new Color(120, 120, 120));
			SetDust(Item, 54);
			AddEffect(Item, ClickEffect.Fritz);

			Item.damage = 6;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 10000;
			Item.rare = 1;
		}
	}
}
