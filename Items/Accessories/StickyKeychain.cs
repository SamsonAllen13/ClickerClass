using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Accessories
{
	public class StickyKeychain : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.StickyKeychain = ClickerSystem.RegisterClickEffect(Mod, "StickyKeychain", null, null, 10, new Color(145, 180, 230, 0), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				int keychain = ModContent.ProjectileType<StickyKeychainPro>();
				Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, keychain, (int)(damage * 0.5), 3f, player.whoAmI, Main.rand.Next(Main.projFrames[keychain]));
			});
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = 25000;
			Item.rare = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().EnableClickEffect(ClickEffect.StickyKeychain);
		}
	}
}
