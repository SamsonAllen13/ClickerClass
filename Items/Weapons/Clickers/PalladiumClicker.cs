using ClickerClass.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class PalladiumClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Palladium Clicker");

			ClickEffect.Regenerate = ClickerSystem.RegisterClickEffect(mod, "Regenerate", "Regenerate", "Grants the Rapid Healing buff, increasing the player's life regeneration", 8, new Color(250, 150, 100, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 24);
				player.AddBuff(BuffID.RapidHealing, 120, false);
				for (int i = 0; i < 15; i++)
				{
					int num6 = Dust.NewDust(player.position, 20, 20, ModContent.DustType<LoveDust>(), 0f, 0f, 0, Color.White, 1f);
					Main.dust[num6].noGravity = true;
					Main.dust[num6].velocity *= 0.75f;
					int num7 = Main.rand.Next(-50, 51);
					int num8 = Main.rand.Next(-50, 51);
					Dust dust = Main.dust[num6];
					dust.position.X = dust.position.X + (float)num7;
					Dust dust2 = Main.dust[num6];
					dust2.position.Y = dust2.position.Y + (float)num8;
					Main.dust[num6].velocity.X = -(float)num7 * 0.075f;
					Main.dust[num6].velocity.Y = -(float)num8 * 0.075f;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.85f);
			SetColor(item, new Color(250, 150, 100, 0));
			SetDust(item, 144);
			AddEffect(item, ClickEffect.Regenerate);

			item.damage = 25;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 92000;
			item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PalladiumBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
