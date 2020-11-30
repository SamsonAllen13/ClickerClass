using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SlickClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Slick Clicker");

			ClickEffect.Splash = ClickerSystem.RegisterClickEffect(mod, "Splash", "Splash", "Creates a fountain of 6 damaging water projectiles", 6, new Color(75, 75, 255, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 86);
				for (int k = 0; k < 6; k++)
				{
					Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-6f, -2f), ModContent.ProjectileType<SlickClickerPro>(), (int)(damage * 0.75f), knockBack, player.whoAmI);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.45f);
			SetColor(item, new Color(75, 75, 255, 0));
			SetDust(item, 33);
			AddEffect(item, ClickEffect.Splash);

			item.damage = 11;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 87500;
			item.rare = 2;
		}
	}
}
