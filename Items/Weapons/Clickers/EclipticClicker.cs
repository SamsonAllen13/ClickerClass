using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class EclipticClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Ecliptic Clicker");

			ClickEffect.Totality = ClickerSystem.RegisterClickEffect(mod, "Totality", "Totality", "Creates a miniature eclipse, repeatedly firing out homing projectiles", 15, new Color(255, 200, 100, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 43);
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<TotalityClickerPro>(), damage, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 3.5f);
			SetColor(item, new Color(255, 200, 100, 0));
			SetDust(item, 264);
			AddEffect(item, ClickEffect.Totality);

			item.damage = 48;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 165000;
			item.rare = 6;
		}
	}
}
