using ClickerClass.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CobaltClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Haste = ClickerSystem.RegisterClickEffect(mod, "Haste", null, null, 5, new Color(50, 125, 200), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 24);
				player.AddBuff(ModContent.BuffType<Haste>(), 300, false);
				for (int i = 0; i < 15; i++)
				{
					int num6 = Dust.NewDust(player.position, 20, 20, 56, 0f, 0f, 150, default(Color), 1.25f);
					Main.dust[num6].noGravity = true;
					Main.dust[num6].velocity *= 0.75f;
					int num7 = Main.rand.Next(-50, 51);
					int num8 = Main.rand.Next(-50, 51);
					Dust dust = Main.dust[num6];
					dust.position.X += num7;
					Dust dust2 = Main.dust[num6];
					dust2.position.Y += num8;
					Main.dust[num6].velocity.X = -num7 * 0.075f;
					Main.dust[num6].velocity.Y = -num8 * 0.075f;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.75f);
			SetColor(item, new Color(50, 125, 200));
			SetDust(item, 48);
			AddEffect(item, ClickEffect.Haste);

			item.damage = 24;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 45000;
			item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CobaltBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
