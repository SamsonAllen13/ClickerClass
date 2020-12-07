using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CandleClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Candle Clicker");

			ClickEffect.Illuminate = ClickerSystem.RegisterClickEffect(mod, "Illuminate", "Illuminate", "Creates 8 sparks along nearby tiles that illuminates the area", 10, new Color(255, 175, 75, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 74);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 55, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 255, default, 1.35f);
					dust.noGravity = true;
				}
				for (int k = 0; k < 8; k++)
				{
					Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), ModContent.ProjectileType<CandleClickerPro>(), 0, 0f, player.whoAmI);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 1.5f);
			SetColor(item, new Color(255, 175, 75, 0));
			SetDust(item, 55);
			AddEffect(item, ClickEffect.Illuminate);

			item.damage = 8;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 25000;
			item.rare = 2;
		}
	}
}
