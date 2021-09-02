using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class EclipticClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Totality = ClickerSystem.RegisterClickEffect(Mod, "Totality", null, null, 15, new Color(255, 200, 100), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<TotalityClickerPro>(), damage, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.5f);
			SetColor(Item, new Color(255, 200, 100));
			SetDust(Item, 264);
			AddEffect(Item, ClickEffect.Totality);

			Item.damage = 28;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 165000;
			Item.rare = 6;
		}
	}
}
