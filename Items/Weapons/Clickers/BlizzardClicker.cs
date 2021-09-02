using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class BlizzardClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Flurry = ClickerSystem.RegisterClickEffect(mod, "Flurry", null, null, 15, new Color(150, 220, 255), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<BlizzardClickerPro>(), damage, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 3f);
			SetColor(item, new Color(150, 220, 255));
			SetDust(item, 92);
			AddEffect(item, ClickEffect.Flurry);

			item.damage = 26;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 105000;
			item.rare = 5;
		}
	}
}
