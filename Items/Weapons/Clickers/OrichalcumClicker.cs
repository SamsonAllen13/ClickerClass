using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class OrichalcumClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.PetalStorm = ClickerSystem.RegisterClickEffect(Mod, "PetalStorm", null, null, 10, new Color(255, 150, 255), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(SoundID.Item24, position);

				for (int k = 0; k < 5; k++)
				{
					float xChoice = Main.rand.Next(-100, 101);
					float yChoice = Main.rand.Next(-100, 101);
					xChoice += xChoice > 0 ? 300 : -300;
					yChoice += yChoice > 0 ? 300 : -300;
					Vector2 startSpot = new Vector2(position.X + xChoice, position.Y + yChoice);
					Vector2 endSpot = new Vector2(position.X + Main.rand.Next(-10, 11), position.Y + Main.rand.Next(-10, 11));
					Vector2 vector = endSpot - startSpot;
					float speed = 4f;
					float mag = vector.Length();
					if (mag > speed)
					{
						mag = speed / mag;
						vector *= mag;
					}

					int orichalcum = ModContent.ProjectileType<OrichaclumClickerPro>();
					Projectile.NewProjectile(source, startSpot, vector, orichalcum, (int)(damage * 0.5f), 0f, player.whoAmI, Main.rand.Next(Main.projFrames[orichalcum]), 0f);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3f);
			SetColor(Item, new Color(255, 150, 255));
			SetDust(Item, 145);
			AddEffect(Item, ClickEffect.PetalStorm);

			Item.damage = 22;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 2, 53, 0);
			Item.rare = ItemRarityID.LightRed;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.OrichalcumBar, 8).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
