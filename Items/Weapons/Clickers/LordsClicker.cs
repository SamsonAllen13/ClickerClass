using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class LordsClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Lord's Clicker");

			ClickEffect.Conqueror = ClickerSystem.RegisterClickEffect(mod, "Conqueror", "Conqueror", "Creates an area-of-effect phantasmal explosion that deals double damage and a guaranteed critical hit", 15, new Color(100, 255, 200, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 88);
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<LordsClickerPro>(), (int)(damage * 2f), 0f, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 6f);
			SetColor(item, new Color(100, 255, 200, 0));
			SetDust(item, 110);
			AddEffect(item, ClickEffect.Conqueror);

			item.damage = 122;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 10;
		}
	}
}
