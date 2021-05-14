using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class BigRedButton : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.BigRedButton = ClickerSystem.RegisterClickEffect(mod, "BigRedButton", null, null, 25, new Color(230, 100, 20, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)			{
				Main.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 14);
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<BigRedButtonPro>(), (int)(damage * 3f), 1f, player.whoAmI, Main.rand.Next(3));
				for (int k = 0; k < 6; k++)
				{
					Vector2 spread = new Vector2(-0.5f, -3f);
					if (k == 1){spread = new Vector2(-0.25f, -2.5f);}
					if (k == 2){spread = new Vector2(-0.25f, -3.5f);}
					if (k == 3){spread = new Vector2(0.25f, -3.5f);}
					if (k == 4){spread = new Vector2(0.25f, -2.5f);}
					if (k == 5){spread = new Vector2(0.5f, -3f);}
					Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, spread.X, spread.Y, ModContent.ProjectileType<BigRedButtonPro2>(), damage, 0f, player.whoAmI);
				}
				for (int k = 0; k < 20; k++)
				{
					Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 174, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 0, default, 1.75f);
					dust.noGravity = true;
					dust.noLight = true;
				}
			});
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 250000;
			item.rare = 4;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().EnableClickEffect(ClickEffect.BigRedButton);
		}
	}
}
