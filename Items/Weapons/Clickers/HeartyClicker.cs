using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class HeartyClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.HappyHeart = ClickerSystem.RegisterClickEffect(Mod, "HappyHeart", 20, new Color(255, 170, 235), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					Projectile projectile = Main.projectile[i];
					if (projectile.active && projectile.owner == player.whoAmI && projectile.type == ModContent.ProjectileType<HeartyClickerPro>())
					{
						projectile.ai[1] += 1f;
					}
				}
				Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-2f, 2f), -6f, ModContent.ProjectileType<HeartyClickerPro>(), 0, 0f, player.whoAmI, 1f);
			},
			preHardMode: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.5f);
			SetColor(Item, new Color(255, 170, 235));
			SetDust(Item, 117);
			AddEffect(Item, ClickEffect.HappyHeart);

			Item.damage = 7;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 2, 0, 0);
			Item.rare = ItemRarityID.Orange;
		}
		
		
	}
}
