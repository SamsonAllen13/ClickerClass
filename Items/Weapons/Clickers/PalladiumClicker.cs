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

			ClickEffect.Regenerate = ClickerSystem.RegisterClickEffect(mod, "Regenerate", null, null, 8, new Color(250, 150, 100), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 24);
				player.AddBuff(BuffID.RapidHealing, 120, false);
				for (int i = 0; i < 15; i++)
				{
					int index = Dust.NewDust(player.position, player.width, player.height, ModContent.DustType<LoveDust>(), 0f, 0f, 0, Color.White, 1f);
					Dust dust = Main.dust[index];
					dust.noGravity = true;
					dust.velocity *= 0.75f;
					int x = Main.rand.Next(-50, 51);
					int y = Main.rand.Next(-50, 51);
					dust.position.X += x;
					dust.position.Y += y;
					dust.velocity.X = -x * 0.075f;
					dust.velocity.Y = -y * 0.075f;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.85f);
			SetColor(item, new Color(250, 150, 100));
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
