using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class BalloonClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.BalloonDefense = ClickerSystem.RegisterClickEffect(Mod, "BalloonDefense", null, null, 20, new Color(200, 125, 125), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, Main.MouseWorld.X, Main.MouseWorld.Y - 500, 0f, 15f, ModContent.ProjectileType<BalloonClickerPro>(), damage, 0f, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.15f);
			SetColor(Item, new Color(180, 75, 75));
			SetDust(Item, 36);
			AddEffect(Item, ClickEffect.BalloonDefense);

			Item.damage = 4;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 5000;
			Item.rare = 1;
		}
	}
}
