using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class StarryClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Starfall = ClickerSystem.RegisterClickEffect(Mod, "Starfall", null, null, 15, new Color(255, 50, 200), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, Main.MouseWorld.X, Main.MouseWorld.Y - 500, 0f, 15f, ModContent.ProjectileType<StarryClickerPro>(), (int)(damage * 1.5f), 1f, player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.5f);
			SetColor(Item, new Color(255, 60, 210));
			SetDust(Item, 71);
			AddEffect(Item, ClickEffect.Starfall);

			Item.damage = 6;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 25000;
			Item.rare = 2;
		}
	}
}
