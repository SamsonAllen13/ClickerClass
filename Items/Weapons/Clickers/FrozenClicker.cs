using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class FrozenClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Freeze = ClickerSystem.RegisterClickEffect(Mod, "Freeze", null, null, 1, new Color(175, 255, 255), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<FrozenClickerPro>(), 0, 0f, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6f);
			SetColor(Item, new Color(175, 255, 255));
			SetDust(Item, 92);
			AddEffect(Item, ClickEffect.Freeze);

			Item.damage = 82;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 500000;
			Item.rare = 8;
		}
	}
}
