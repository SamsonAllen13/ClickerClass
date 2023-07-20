using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class StickyKeychain : ClickerItem
	{
		public static readonly int ClickAmount = 10;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ClickAmount);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.StickyKeychain = ClickerSystem.RegisterClickEffect(Mod, "StickyKeychain", ClickAmount, new Color(145, 180, 230, 0), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				int keychain = ModContent.ProjectileType<StickyKeychainPro>();
				Projectile.NewProjectile(source, position, Vector2.Zero, keychain, (int)(damage * 0.5), 3f, player.whoAmI, Main.rand.Next(Main.projFrames[keychain]));
			},
			preHardMode: true);
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 0, 50, 0);
			Item.rare = ItemRarityID.Blue;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().EnableClickEffect(ClickEffect.StickyKeychain);
		}
	}
}
