using ClickerClass.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class ShroomiteClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.AutoClick = ClickerSystem.RegisterClickEffect(mod, "AutoClick", null, null, 20, new Color(150, 150, 255), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				player.GetModPlayer<ClickerPlayer>().clickAmount++;
				Main.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 24);
				player.AddBuff(ModContent.BuffType<AutoClick>(), 300, false);
				for (int i = 0; i < 15; i++)
				{
					int num6 = Dust.NewDust(player.position, 20, 20, 15, 0f, 0f, 255, default(Color), 1.5f);
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
			SetRadius(item, 6f);
			SetColor(item, new Color(150, 150, 255));
			SetDust(item, 113);
			AddEffect(item, ClickEffect.AutoClick);

			item.damage = 68;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 450000;
			item.rare = 8;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ShroomiteBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
