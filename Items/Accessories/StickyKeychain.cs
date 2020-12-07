using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class StickyKeychain : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Tooltip.SetDefault("Every 10 clicks sticks damaging slime on to your screen");

			ClickEffect.StickyKeychain = ClickerSystem.RegisterClickEffect(mod, "StickyKeychain", "Sticky Keychain", "Sticks damaging slime on to your screen", 10, new Color(145, 180, 230, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(3, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 13);
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<StickyKeychainPro>(), (int)(damage * 0.5), 3f, player.whoAmI, Main.rand.Next(3));
				for (int k = 0; k < 20; k++)
				{
					Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 88, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 175, default, 1.75f);
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
			item.value = 25000;
			item.rare = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().EnableClickEffect(ClickEffect.StickyKeychain);
		}
	}
}
