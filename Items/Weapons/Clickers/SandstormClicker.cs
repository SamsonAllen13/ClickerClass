using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SandstormClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.DustDevil = ClickerSystem.RegisterClickEffect(mod, "DustDevil", null, null, 10, new Color(220, 185, 120), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 8);
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<SandstormClickerPro>(), damage, knockBack, player.whoAmI);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 216, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 150, default, 1.25f);
					dust.noGravity = true;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 3.1f);
			SetColor(item, new Color(225, 170, 125));
			SetDust(item, 216);
			AddEffect(item, ClickEffect.DustDevil);

			item.damage = 30;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 105000;
			item.rare = 5;
		}
	}
}
