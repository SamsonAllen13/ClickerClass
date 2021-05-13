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

			ClickEffect.StickyKeychain = ClickerSystem.RegisterClickEffect(mod, "StickyKeychain", null, null, 10, new Color(145, 180, 230, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				int keychain = ModContent.ProjectileType<StickyKeychainPro>();
				Projectile.NewProjectile(Main.MouseWorld, Vector2.Zero, keychain, (int)(damage * 0.5), 3f, player.whoAmI, Main.rand.Next(Main.projFrames[keychain]));
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
