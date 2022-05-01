using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SandstormClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.DustDevil = ClickerSystem.RegisterClickEffect(Mod, "DustDevil", null, null, 10, new Color(220, 185, 120), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<SandstormClickerPro>(), damage, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.1f);
			SetColor(Item, new Color(225, 170, 125));
			SetDust(Item, 216);
			AddEffect(Item, ClickEffect.DustDevil);

			Item.damage = 26;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 105000;
			Item.rare = 5;
		}
	}
}
