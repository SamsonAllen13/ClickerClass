using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class TheClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.TheClick = ClickerSystem.RegisterClickEffect(mod, "TheClick", null, null, 1, new Color(255, 255, 255), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<TheClickerPro>(), damage, 0f, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 6f);
			SetColor(item, new Color(255, 255, 255));
			SetDust(item, 91);
			AddEffect(item, ClickEffect.TheClick);

			item.damage = 130;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 10;
		}
	}
}
