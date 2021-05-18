using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class MyceliumClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Spores = ClickerSystem.RegisterClickEffect(mod, "Spores", null, null, 10, new Color(115, 150, 220), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(3, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 13);
				for (int k = 0; k < 5; k++)
				{
					Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-4f, -2f), ModContent.ProjectileType<MyceliumClickerPro>(), (int)(damage * 0.5f), 2f, player.whoAmI);
				}
				for (int k = 0; k < 20; k++)
				{
					Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 41, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 200, default, 1.25f);
					dust.noGravity = true;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 1.6f);
			SetColor(item, new Color(115, 150, 220));
			SetDust(item, 224);
			AddEffect(item, ClickEffect.Spores);

			item.damage = 9;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 10000;
			item.rare = 1;
		}
	}
}
