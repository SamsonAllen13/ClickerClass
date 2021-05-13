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
					int index = Dust.NewDust(player.position, player.width, player.height, 56, 0f, 0f, 150, default(Color), 1.25f);
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
