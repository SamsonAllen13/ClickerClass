using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SeafoamClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.SeaSpray = ClickerSystem.RegisterClickEffect(Mod, "SeaSpray", null, null, 15, new Color(160, 230, 200), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<SeafoamClickerPro>(), 0, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 5.85f);
			SetColor(Item, new Color(95, 210, 160));
			SetDust(Item, 99);
			AddEffect(Item, ClickEffect.SeaSpray);

			Item.damage = 58;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 500000;
			Item.rare = 8;
		}
	}
}
