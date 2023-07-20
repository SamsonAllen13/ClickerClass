using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class BigRedButton : ClickerItem
	{
		public static readonly int ClickAmount = 25;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ClickAmount);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.BigRedButton = ClickerSystem.RegisterClickEffect(Mod, "BigRedButton", ClickAmount, new Color(230, 100, 20, 0), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(SoundID.Item14, position);
				Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<BigRedButtonPro>(), (int)(damage * 3f), 1f, player.whoAmI, Main.rand.Next(3));
				for (int k = 0; k < 6; k++)
				{
					Vector2 spread = new Vector2(-0.5f, -3f);
					if (k == 1) { spread = new Vector2(-0.25f, -2.5f); }
					if (k == 2) { spread = new Vector2(-0.25f, -3.5f); }
					if (k == 3) { spread = new Vector2(0.25f, -3.5f); }
					if (k == 4) { spread = new Vector2(0.25f, -2.5f); }
					if (k == 5) { spread = new Vector2(0.5f, -3f); }
					Projectile.NewProjectile(source, position, spread, ModContent.ProjectileType<BigRedButtonPro2>(), damage, 0f, player.whoAmI);
				}
				for (int k = 0; k < 20; k++)
				{
					Dust dust = Dust.NewDustDirect(position, 8, 8, 174, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 0, default, 1.75f);
					dust.noGravity = true;
					dust.noLight = true;
				}
			});
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.LightRed;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().EnableClickEffect(ClickEffect.BigRedButton);
		}
	}
}
