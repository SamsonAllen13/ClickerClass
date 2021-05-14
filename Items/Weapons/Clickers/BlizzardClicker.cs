using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class BlizzardClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Flurry = ClickerSystem.RegisterClickEffect(mod, "Flurry", null, null, 15, new Color(150, 220, 255), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 24);
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<BlizzardClickerPro>(), damage, knockBack, player.whoAmI);
				for (int k = 0; k < 15; k++)
				{
					Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 92, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 0, default, 1f);
					dust.noGravity = true;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 3f);
			SetColor(item, new Color(150, 220, 255));
			SetDust(item, 92);
			AddEffect(item, ClickEffect.Flurry);

			item.damage = 29;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 105000;
			item.rare = 5;
		}
	}
}
