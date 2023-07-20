using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class ClearKeychain : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.ClearKeychain = ClickerSystem.RegisterClickEffect(Mod, "ClearKeychain", 15, new Color(225, 200, 255, 0), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(SoundID.Item86, position);

				for (int k = 0; k < 5; k++)
				{
					Vector2 startSpot = new Vector2(position.X + Main.rand.Next(-150, 151), position.Y - 500 + Main.rand.Next(-25, 26));
					Vector2 endSpot = new Vector2(position.X + Main.rand.Next(-50, 51), position.Y + Main.rand.Next(-50, 51));
					Vector2 vector = endSpot - startSpot;
					float speed = 8f + Main.rand.NextFloat(-1f, 1f);
					float mag = vector.Length();
					if (mag > speed)
					{
						mag = speed / mag;
						vector *= mag;
					}
					Projectile.NewProjectile(source, startSpot, vector, ModContent.ProjectileType<ClearKeychainPro>(), 0, 0f, player.whoAmI, endSpot.X, endSpot.Y);
				}
			});
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 0, 70, 0);
			Item.rare = ItemRarityID.Pink;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().EnableClickEffect(ClickEffect.ClearKeychain);
		}
	}
}
