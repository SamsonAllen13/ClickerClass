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

			ClickEffect.Totality = ClickerSystem.RegisterClickEffect(mod, "Totality", null, null, 15, new Color(255, 200, 100), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<TotalityClickerPro>(), damage, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 3.5f);
			SetColor(item, new Color(255, 200, 100));
			SetDust(item, 264);
			AddEffect(item, ClickEffect.Totality);

			item.damage = 28;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 165000;
			item.rare = 6;
		}
	}
}
